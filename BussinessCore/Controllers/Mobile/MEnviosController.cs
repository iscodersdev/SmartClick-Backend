using BusinessCore.Services;
using BussinessCore.API.Filters;
using Commons.Extensions;
using DAL.Data;
using DAL.DTOs.Plenario;
using DAL.Mobile;
using DAL.Models;
using DAL.Models.Core;
using DAL.Models.PSP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Ocsp;
using Serilog;
using SmartClickCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessCore.API.Controllers.Billetera
{
    [TypeFilter(typeof(ChequeaUatApiAttribute))]
    [ApiController]
    [Route("api/[controller]")]
    public class MEnviosController : BaseApiController
    {
        private readonly NotificacionAPIService _notificacionAPIService;
        private readonly IPSPService _pspService;

        public MEnviosController(SmartClickContext context, NotificacionAPIService notificacionAPIService, IPSPService pspService) : base(context)
        {
            _notificacionAPIService = notificacionAPIService;
            _pspService = pspService;
        }


        [HttpPost("EnvioBilletera")]
        public async Task<IActionResult> EnvioBilletera([FromBody] EnvioBilleteraDTO envioBilleteraDTO)
        {
            try
            {
                DAL.Models.Core.Billetera billeteraDestino = null;
                if (envioBilleteraDTO.Monto.Contains(","))
                {
                    Log.Error($"Error de monto debe usar puntos para decimales");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = envioBilleteraDTO.UAT, Mensaje = "Error en monto de envio, debe usar el punto para decimales" });
                }

                if (!Decimal.TryParse(envioBilleteraDTO.Monto, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out decimal montoEnvio))
                {
                    Log.Error($"Error de monto en envio entre billeteras");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = envioBilleteraDTO.UAT, Mensaje = "Error en monto de envio" });
                }


                var usuario = TraeUsuarioUAT(envioBilleteraDTO.UAT);

                var billeteraOrigen = TraeBilletera(usuario);
                if (!billeteraOrigen.ChequeaDebito(montoEnvio))
                {
                    Log.Error($"Error el monto supera el saldo");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = envioBilleteraDTO.UAT, Mensaje = "El monto supera su saldo" });
                }

                var pspAccountDestino = _context.PSPAccounts.Where(b => b.CVU == envioBilleteraDTO.CVU).FirstOrDefault();

                if (pspAccountDestino!=null)
                {
                    billeteraDestino = _context.Billeteras.Where(b => b.Cliente.Id == pspAccountDestino.Cliente.Id).FirstOrDefault();

                    if (billeteraDestino==null)
                    {
                        return new JsonResult(new DAL.Models.RespuestaAPI { Status = 500, UAT = envioBilleteraDTO.UAT, Mensaje = "Error no existe Billetera" });
                    }
                }
                

                if (pspAccountDestino==null) // no es una billetera de Smart-click
                {                    
                    var pspAccount = TraeAccountPSP(usuario);
                    string decryptedToken = common.DescifrarPassword(pspAccount.EncryptedPassword);

                    var AccesToken = await _pspService.GetAccessTokenUserAsync(pspAccount.UserName, decryptedToken);

                    ExternalAccountDataDTO pspResp = _pspService.ValidarCuentaExternaAsync(envioBilleteraDTO.CVU, AccesToken.access_token).Result;
                    
                    Console.WriteLine(pspResp);
                    
                    if (pspResp.CUIT!=pspAccount.TributaryIdentifier)
                    {
                        return new JsonResult(new DAL.Models.RespuestaAPI { Status = 500, UAT = envioBilleteraDTO.UAT, Mensaje = "La cuenta origen y destino no son del mismo titular." });
                    }
                    
                    int externalAccountId = pspResp.ExternoId;
                    AgendarCuentaDataDTO respuestaAgendar =
                        _pspService.AgendarCuentaExternaAsync(externalAccountId, AccesToken.access_token).Result;
                    Console.WriteLine(respuestaAgendar);

                    if (!respuestaAgendar.Success)
                    {
                        return new JsonResult(new DAL.Models.RespuestaAPI { Status = 500, UAT = envioBilleteraDTO.UAT, Mensaje = "No se pudo agendar correctamente la cuenta externa." });
                    }

                    var respuestaRecaudadora = _pspService.TransferenciaCuentaRecaudadoraAsync(pspAccount, envioBilleteraDTO.Monto).Result;

                    Console.WriteLine(respuestaRecaudadora);
                    
                    if (!respuestaRecaudadora.Success)
                    {
                        return new JsonResult(new DAL.Models.RespuestaAPI { Status = 500, UAT = envioBilleteraDTO.UAT, Mensaje = "Error en envio" });
                    }
                  

                    if (pspResp.Success)
                    {
                        var solicitudTransferencia = _pspService.SolicitudDeTransferenciaAsync(pspAccount, pspResp, true, envioBilleteraDTO.Monto, AccesToken.access_token).Result;

                        Console.WriteLine(solicitudTransferencia);
                        
                        TransactionConfirmationRequestDTO confirmarTrans = new TransactionConfirmationRequestDTO()
                        {
                            Guid = new ConfirmationGuidDTO()
                            {
                                Key = solicitudTransferencia.Guid.Key,
                            },                           
                            OTP = 999999,
                            TransactionId = solicitudTransferencia.Data.TransactionId,
                            IsExternal = true
                        };

                        if (solicitudTransferencia.Success)
                        {
                            var confirmarTransferencia = _pspService.ConfirmarTransferenciaAsync(confirmarTrans, AccesToken.access_token).Result;
                            if (!confirmarTransferencia.Success)
                            {
                                return new JsonResult(new DAL.Models.RespuestaAPI { Status = 500, UAT = envioBilleteraDTO.UAT, Mensaje = "Error en envio - "+confirmarTransferencia.Message });
                            }
                        }
                    }
                    else
                    {
                        return new JsonResult(new DAL.Models.RespuestaAPI { Status = 500, UAT = envioBilleteraDTO.UAT, Mensaje = "Error en envio - "+pspResp.Mensaje });
                    }
                }
                else
                {
                    var movimientoDestino = new MovimientoBilletera
                    {
                        CBU = billeteraOrigen.CVU,
                        Fecha = DateTime.Now,
                        Monto = montoEnvio,
                        OrigenAsociado = new OrigenMovimiento
                        {
                            TipoOrigen = TipoOrigenMovimiento.Billetera,
                            IdAsociado = billeteraOrigen.Id,
                            Descripcion = TipoOrigenMovimiento.Billetera.GetDisplayName()
                        },
                        TipoMovimiento = _context.TipoMovimientoBilletera.Find((int)TipoMovimientoBilleteraEnum.IngresoDinero)
                    };


                    billeteraDestino.Saldo += montoEnvio;
                    billeteraDestino.Movimientos.Add(movimientoDestino);
                    billeteraDestino.Contactos.Add(new ContactosBilletera
                    {
                        ClienteContacto = billeteraOrigen.Cliente,
                        Detalle = billeteraOrigen.Cliente.Persona?.GetNombreCompleto()
                    });
                    _context.Update(billeteraDestino);
                }          


                var movimientoOrigen = new MovimientoBilletera
                {
                    CBU = billeteraDestino!=null ? billeteraDestino.CVU : envioBilleteraDTO.CVU,
                    Fecha = DateTime.Now,
                    Monto = montoEnvio,
                    OrigenAsociado = new OrigenMovimiento
                    {
                        TipoOrigen = TipoOrigenMovimiento.Billetera,
                        IdAsociado = billeteraDestino!=null? billeteraDestino.Id:0,
                        Descripcion = TipoOrigenMovimiento.Billetera.GetDisplayName()
                    },
                    TipoMovimiento = _context.TipoMovimientoBilletera.Find((int)TipoMovimientoBilleteraEnum.EnvioBilletera)
                };

                billeteraOrigen.Saldo -= montoEnvio;
                billeteraOrigen.Movimientos.Add(movimientoOrigen);
                if (billeteraDestino!=null)
                {
                    billeteraOrigen.Contactos.Add(new ContactosBilletera
                    {
                        ClienteContacto = billeteraDestino.Cliente,
                        Detalle = billeteraDestino!=null ? billeteraDestino.Cliente.Persona?.GetNombreCompleto() : ""
                    });
                    _notificacionAPIService.Envia_Push(billeteraDestino.Cliente.Usuario.DeviceId, "Recepcion de dinero", $"Ha recibido ${montoEnvio} en su billetera");
                }

                _context.Update(billeteraOrigen);
                await _context.SaveChangesAsync();
                _notificacionAPIService.Envia_Push(billeteraOrigen.Cliente.Usuario.DeviceId, "Envio de dinero", $"Ha enviado ${montoEnvio} exitosamente");
                return new JsonResult(new DAL.Models.RespuestaAPI { Status = 200, UAT = envioBilleteraDTO.UAT, Mensaje = "Envio realizado" });

            }
            catch (Exception e)
            {
                Log.Error($"Error en envio entre billeteras - {e.Message}");
                return new JsonResult(new DAL.Models.RespuestaAPI { Status = 500, UAT = envioBilleteraDTO.UAT, Mensaje = "Error en envio" });
            }
        }

        [HttpPost("IngresoDinero")]
        public async Task<IActionResult> IngresoDinero([FromBody] IngresoDineroDTO ingresoDineroDTO)
        {
            try
            {

                if (ingresoDineroDTO.Monto.Contains(","))
                {
                    Log.Error($"Error de monto debe usar puntos para decimales");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = ingresoDineroDTO.UAT, Mensaje = "Error en monto de envio, debe usar el punto para decimales" });
                }

                if (!Decimal.TryParse(ingresoDineroDTO.Monto, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out decimal montoIngreso))
                {
                    Log.Error($"Error de monto en ingreso de dinero");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = ingresoDineroDTO.UAT, Mensaje = "Error en monto de ingreso" });
                }

                var movimiento = new MovimientoBilletera
                {
                    Fecha = DateTime.Now,
                    Monto = montoIngreso,
                    TipoMovimiento = _context.TipoMovimientoBilletera.Find((int)TipoMovimientoBilleteraEnum.IngresoDinero)
                };

                var billetera = TraeBilletera(TraeUsuarioUAT(ingresoDineroDTO.UAT));
                var exito = true;
                switch (ingresoDineroDTO.TipoMedioPago)
                {
                    case TipoMedioPago.TipoTarjeta:
                        var tarjetaPagadora = billetera.Tarjetas.Where(t => t.Id == ingresoDineroDTO.MetodoPagoId).FirstOrDefault();
                        tarjetaPagadora.Movimientos.Add(movimiento);
                        break;
                    case TipoMedioPago.TipoCuenta:
                        var cuentaPagadora = billetera.Cuentas.Where(c => c.Id == ingresoDineroDTO.MetodoPagoId).FirstOrDefault();
                        cuentaPagadora.Movimientos.Add(movimiento);
                        break;
                    default:
                        exito = false;
                        break;
                }
                if (exito)
                {
                    billetera.Saldo += montoIngreso;
                    _context.Update(billetera);
                    _context.SaveChanges();
                    _notificacionAPIService.Envia_Push(billetera.Cliente.Usuario.DeviceId, "Ingreso de dinero", $"Ha ingresado ${montoIngreso} en su billetera");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 200, UAT = ingresoDineroDTO.UAT, Mensaje = "Ingreso de dinero efectuado" });
                }
                else
                {
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = ingresoDineroDTO.UAT, Mensaje = "No se pudo registrar el ingreso" });
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error en el ingreso de dinero - {e.Message}");
                return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = ingresoDineroDTO.UAT, Mensaje = "Error en el ingreso de dinero" });
            }

        }

        [HttpPost("RetiroDinero")]
        public async Task<IActionResult> RetiroDinero([FromBody] RetiroDineroDTO retiroDineroDTO)
        {
            try
            {


                if (retiroDineroDTO.Monto.Contains(","))
                {
                    Log.Error($"Error de monto debe usar puntos para decimales");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = retiroDineroDTO.UAT, Mensaje = "Error en monto, debe usar el punto para decimales" });
                }

                if (!Decimal.TryParse(retiroDineroDTO.Monto, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out decimal montoRetiro))
                {
                    Log.Error($"Error de monto en retiro de dinero");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = retiroDineroDTO.UAT, Mensaje = "Error en monto" });
                }

                var billetera = TraeBilletera(TraeUsuarioUAT(retiroDineroDTO.UAT));

                if (!billetera.ChequeaDebito(montoRetiro))
                {
                    Log.Error($"Error el monto supera el saldo");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = retiroDineroDTO.UAT, Mensaje = "El monto supera su saldo" });
                }

                var movimiento = new MovimientoBilletera
                {
                    Fecha = DateTime.Now,
                    Monto = montoRetiro,
                    TipoMovimiento = _context.TipoMovimientoBilletera.Find((int)TipoMovimientoBilleteraEnum.RetiroDinero)
                };

                var exito = true;
                switch (retiroDineroDTO.TipoMedioPago)
                {
                    case TipoMedioPago.TipoTarjeta:
                        var tarjetaPagadora = billetera.Tarjetas.Where(t => t.Id == retiroDineroDTO.MetodoPagoId).FirstOrDefault();
                        tarjetaPagadora.Movimientos.Add(movimiento);
                        break;
                    case TipoMedioPago.TipoCuenta:
                        var cuentaPagadora = billetera.Cuentas.Where(c => c.Id == retiroDineroDTO.MetodoPagoId).FirstOrDefault();
                        cuentaPagadora.Movimientos.Add(movimiento);
                        break;
                    default:
                        exito = false;
                        break;
                }
                if (exito)
                {
                    billetera.Saldo -= montoRetiro;
                    _context.Update(billetera);
                    _context.SaveChanges();
                    _notificacionAPIService.Envia_Push(billetera.Cliente.Usuario.DeviceId, "Ingreso de dinero", $"Ha retirado ${montoRetiro} en su billetera");
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 200, UAT = retiroDineroDTO.UAT, Mensaje = "Retiro de dinero efectuado" });
                }
                else
                {
                    return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = retiroDineroDTO.UAT, Mensaje = "No se pudo registrar el retiro" });
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error en el ingreso de dinero - {e.Message}");
                return new JsonResult(new DAL.Models.RespuestaAPI { Status = 400, UAT = retiroDineroDTO.UAT, Mensaje = "Error en el retiro de dinero" });
            }

        }


    }
}
