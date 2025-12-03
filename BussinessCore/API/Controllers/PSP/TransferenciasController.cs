using BusinessCore.Services;
using Commons.Extensions; // for GetDisplayName extension
using DAL.Data;
using DAL.DTOs.API;
using DAL.DTOs.Plenario;
using DAL.DTOs.PSP;
using DAL.Mobile;
using DAL.Models;
using DAL.Models.Core; // for MovimientoBilletera, OrigenMovimiento, TipoOrigenMovimiento
using DAL.Models.PSP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartClickCore.API.Controllers.PSP
{
    [ApiController]
    [Route("api/psp/[controller]")]
    public class TransferenciasController : ControllerBase
    {
        private readonly IPSPService _pspService;
        private readonly SmartClickContext _context;
        private readonly NotificacionAPIService _notificacionAPIService;

        public TransferenciasController(SmartClickContext context, NotificacionAPIService notificacionAPIService, IPSPService pspService)
        {
            _context = context;
            _pspService = pspService;
            _notificacionAPIService = notificacionAPIService;
        }

        // Método corregido para buscar usuario por UAT
        private DAL.Models.Usuario TraeUsuarioUAT(string uat)
        {
            return _context.UAT.Where(u => u.Token == uat).Select(u => u.Cliente.Usuario).FirstOrDefault();
        }

        public DAL.Models.Core.Billetera TraeBilleteraCVU(string cvu)
        {
            return _context.Billeteras.Where(b => b.CVU == cvu).FirstOrDefault();
        }

        // *** NUEVO MÉTODO HELPER: OBTENER O REFRESCAR TOKEN DEL USUARIO PSP ***
        /// <summary>
        /// Obtiene el UserToken del PSP para un usuario, ya sea desde la base de datos o solicitándolo al PSP
        /// </summary>
        private async Task<string> ObtenerUserTokenPSP(DAL.Models.Usuario usuario)
        {
            try
            {
                Log.Information($"🔍 ObtenerUserTokenPSP iniciado para usuario: {usuario.UserName} (ID: {usuario.Id})");

                // 1. Buscar PSPAccount existente para el usuario
                var pspAccount = _context.Set<DAL.Models.PSPAccount>()
                    .Where(p => p.Usuario.Id == usuario.Id)
                    .FirstOrDefault();

                if (pspAccount == null)
                {
                    Log.Warning($"⚠️ No existe PSPAccount en BD para usuario {usuario.UserName} (ID: {usuario.Id})");
                }
                else
                {
                    Log.Information($"✅ PSPAccount encontrado - UserName: {pspAccount.UserName}, PSPUserId: {pspAccount.PSPUserId}");
                    Log.Information($"   - Token almacenado: {(!string.IsNullOrEmpty(pspAccount.EncryptedUserToken) ? "SÍ" : "NO")}");
                    Log.Information($"   - Token expira: {pspAccount.TokenExpiry?.ToString() ?? "N/A"}");
                    Log.Information($"   - Password almacenado: {(!string.IsNullOrEmpty(pspAccount.EncryptedPassword) ? "SÍ" : "NO")}");
                }

                // 2. Si existe y tiene token válido (no expirado), usarlo
                if (pspAccount != null &&
                    !string.IsNullOrEmpty(pspAccount.EncryptedUserToken) &&
                    pspAccount.TokenExpiry.HasValue &&
                    pspAccount.TokenExpiry.Value > DateTime.UtcNow.AddMinutes(5)) // Buffer de 5 minutos
                {
                    try
                    {
                        string decryptedToken = common.DescifrarPassword(pspAccount.EncryptedUserToken);
                        Log.Information($"✅ Token recuperado desde BD para usuario {usuario.UserName} (válido hasta {pspAccount.TokenExpiry})");
                        return decryptedToken;
                    }
                    catch (Exception ex)
                    {
                        Log.Warning(ex, $"⚠️ Error descifrando token almacenado para usuario {usuario.UserName}");
                    }
                }
                else if (pspAccount != null)
                {
                    if (string.IsNullOrEmpty(pspAccount.EncryptedUserToken))
                    {
                        Log.Warning($"⚠️ PSPAccount existe pero NO tiene token almacenado para usuario {usuario.UserName}");
                    }
                    else if (!pspAccount.TokenExpiry.HasValue)
                    {
                        Log.Warning($"⚠️ PSPAccount tiene token pero sin fecha de expiración para usuario {usuario.UserName}");
                    }
                    else if (pspAccount.TokenExpiry.Value <= DateTime.UtcNow.AddMinutes(5))
                    {
                        Log.Warning($"⚠️ Token expirado o por expirar para usuario {usuario.UserName} (expira: {pspAccount.TokenExpiry})");
                    }
                }

                // 3. Si no hay token válido, necesitamos obtener uno del PSP
                // OPCIÓN A: Si tenemos username y password PSP almacenados en PSPAccount
                if (pspAccount != null &&
                    !string.IsNullOrEmpty(pspAccount.UserName) &&
                    !string.IsNullOrEmpty(pspAccount.EncryptedPassword))
                {
                    try
                    {
                        string decryptedPassword = common.DescifrarPassword(pspAccount.EncryptedPassword);
                        Log.Information($"🔄 Intentando obtener token del PSP para usuario {pspAccount.UserName}");

                        var tokenResponse = await _pspService.GetAccessTokenUserAsync(pspAccount.UserName, decryptedPassword);

                        if (!string.IsNullOrEmpty(tokenResponse?.access_token))
                        {
                            // Guardar el nuevo token en la BD
                            try
                            {
                                pspAccount.EncryptedUserToken = common.CifrarPassword(tokenResponse.access_token);
                                pspAccount.TokenExpiry = tokenResponse.expires_in > 0
                                    ? (DateTime?)DateTime.UtcNow.AddSeconds(tokenResponse.expires_in)
                                    : null;
                                pspAccount.UpdatedAt = DateTime.UtcNow;

                                _context.SaveChanges();

                                Log.Information($"✅ Token del PSP obtenido y guardado para usuario {pspAccount.UserName} (expira: {pspAccount.TokenExpiry})");
                                return tokenResponse.access_token;
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"❌ Error guardando token en BD para usuario {pspAccount.UserName}");
                                // Aunque no se guarde, devolvemos el token obtenido
                                Log.Information($"⚠️ Token obtenido pero no guardado, se devuelve de todas formas");
                                return tokenResponse.access_token;
                            }
                        }
                        else
                        {
                            Log.Error($"❌ PSP retornó token vacío o nulo para usuario {pspAccount.UserName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"❌ Error obteniendo token del PSP para usuario {pspAccount.UserName}");
                    }
                }
                else
                {
                    Log.Warning($"⚠️ No hay credenciales PSP (UserName/Password) guardadas para usuario {usuario.UserName}");
                }

                // OPCIÓN B: Si tenemos un email y password almacenado en Cliente (fallback)
                var cliente = usuario.Clientes;
                if (cliente != null && !string.IsNullOrEmpty(cliente.Password))
                {
                    Log.Information($"🔄 Fallback: Intentando obtener token del PSP usando datos del Cliente para usuario {usuario.UserName}");

                    var tokenResponse = await _pspService.GetAccessTokenUserAsync(usuario.UserName, cliente.Password);

                    if (!string.IsNullOrEmpty(tokenResponse?.access_token))
                    {
                        // Guardar o actualizar el token en la BD
                        if (pspAccount == null)
                        {
                            pspAccount = new DAL.Models.PSPAccount
                            {
                                Usuario = usuario,
                                UserName = usuario.UserName,
                                Status = "active",
                                CreatedAt = DateTime.UtcNow
                            };
                            _context.Set<DAL.Models.PSPAccount>().Add(pspAccount);
                            Log.Information($"✅ Creado nuevo PSPAccount para usuario {usuario.UserName}");
                        }

                        try
                        {
                            pspAccount.EncryptedUserToken = common.CifrarPassword(tokenResponse.access_token);
                            pspAccount.TokenExpiry = tokenResponse.expires_in > 0
                                ? (DateTime?)DateTime.UtcNow.AddSeconds(tokenResponse.expires_in)
                                : null;
                            pspAccount.UpdatedAt = DateTime.UtcNow;

                            _context.SaveChanges();

                            Log.Information($"✅ Token del PSP obtenido y guardado (fallback) para usuario {usuario.UserName}");
                            return tokenResponse.access_token;
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, $"❌ Error guardando token en BD (fallback) para usuario {usuario.UserName}");
                            // Aunque no se guarde, devolvemos el token obtenido
                            return tokenResponse.access_token;
                        }
                    }
                    else
                    {
                        Log.Error($"❌ Fallback: PSP retornó token vacío o nulo para usuario {usuario.UserName}");
                    }
                }
                else
                {
                    Log.Warning($"⚠️ Fallback no disponible: Cliente no tiene Password guardado para usuario {usuario.UserName}");
                }

                Log.Error($"❌ No se pudo obtener UserToken para usuario {usuario.UserName} - Todas las opciones agotadas");
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"❌ Error CRÍTICO en ObtenerUserTokenPSP para usuario {usuario?.UserName}");
                return null;
            }
        }

        /// <summary>
        /// POST api/psp/Transferencias/ValidarCuentaExternaCBU
        /// Valida cuantas externas
        /// </summary>
        [HttpPost("ValidarCuentaExternaCBU")]
        public async Task<IActionResult> ValidarCuentaExternaCBU([FromBody] ValidarCuantaExterna request)
        {
            try
            {
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new { success = false, message = "Usuario no autenticado", data = "", code = "" });
                }

                if (string.IsNullOrEmpty(request.CBU))
                {
                    return BadRequest(new { success = false, message = "Parámetros incompletos", data = "", code = "" });
                }

                // Obtener token del usuario PSP
                var userToken = await ObtenerUserTokenPSP(usuario);
                if (string.IsNullOrEmpty(userToken))
                {
                    return BadRequest(new { Status = 400, UAT = request?.UAT, Mensaje = "No se pudo obtener el token del usuario PSP", Success = false });
                }

                var pspResp = await _pspService.ValidarCuentaExternaAsync(request.CBU, userToken);
                if (pspResp.Status==200)
                {
                    return Ok(pspResp);
                }
                else
                {
                    return StatusCode(pspResp.Status, new { success = false, message = pspResp.Mensaje, data = "", code = "" });
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en ResetPassword");
                return StatusCode(500, new { success = false, message = "Error interno del servidor", data = "", code = "" });
            }
        }


        /// <summary>
        /// POST api/psp/Transferencias/GenerarTransferencia
        /// Valida cuantas externas
        /// </summary>
        [HttpPost("VerificarTransferenciaPSP")]
        public async Task<IActionResult> VerificarTransferenciaPSP([FromBody] RecibirTransferenciaWebhookDTO request)
        {
            try
            {
                PSPAccount account = _context.PSPAccounts.Where(x=>x.CVU == request.Internal.CVU_CBU).FirstOrDefault();
                if (account==null)
                {
                    return StatusCode(500, new { success = false, message = "No se encuentra la cuenta por ese número de cuenta", data = "", code = "" });
                }

                CuentasRecaudadoras cuentaRecaudadora = _context.CuentasRecaudadoras.Where(x => x.AccountNumber=="30717072509-00000591").FirstOrDefault();

                ExternalAccountDataDTO cuantaDestino = new ExternalAccountDataDTO
                {
                    IdentificadorTributario = cuentaRecaudadora.TributaryIdentifier,
                    CUIT = cuentaRecaudadora.TributaryIdentifier,
                    NumeroDeCuenta = cuentaRecaudadora.AccountNumber,
                    Nombre = "Cuenta Recaudadora",
                    TipoCuentaId = 1,
                    TipoMonedaId = 1,
                };
                
                string decryptedToken = common.DescifrarPassword(account.EncryptedPassword);
                
                var token = _pspService.GetAccessTokenUserAsync(account.UserName, decryptedToken);
                var solicitud = await _pspService.SolicitudDeTransferenciaAsync(account, cuantaDestino, false, request.External.Amount.ToString(), token.Result.access_token);
                
                if (solicitud.Success)
                {
                    TransactionConfirmationRequestDTO confirmarTrans = new TransactionConfirmationRequestDTO()
                    {
                        Guid = new ConfirmationGuidDTO()
                        {
                            Key = solicitud.Guid.Key,
                            Code = 999999
                        },
                        OTP = 999999,
                        TransactionId = solicitud.Data.TransactionId,
                        IsExternal = false
                    };

                    var transferencia = await _pspService.ConfirmarTransferenciaAsync(confirmarTrans, token.Result.access_token);

                    DAL.Models.Core.Billetera billeteraOrigen = _context.Billeteras.Where(x => x.Cliente.Usuario.Id == account.Usuario.Id).FirstOrDefault();

                    Log.Error(transferencia.Message, "Info VerificarTransferenciaPSP");
                    Log.Error(transferencia.Success.ToString(), "Info VerificarTransferenciaPSP");
                    Log.Error(transferencia.Data.ToString(), "Info VerificarTransferenciaPSP");
                    
                    if (transferencia.Success)
                    {
                        var movimientoOrigen = new MovimientoBilletera
                        {
                            CBU = billeteraOrigen.CVU,
                            Fecha = DateTime.Now,
                            Monto = request.External.Amount,
                            OrigenAsociado = new OrigenMovimiento
                            {
                                TipoOrigen = TipoOrigenMovimiento.Billetera,
                                IdAsociado =  0,
                                Descripcion = TipoOrigenMovimiento.Billetera.GetDisplayName()
                            },
                            TipoMovimiento = _context.TipoMovimientoBilletera.Find((int)TipoMovimientoBilleteraEnum.IngresoDineroExterno)
                        };

                        billeteraOrigen.Saldo += request.External.Amount;
                        billeteraOrigen.Movimientos.Add(movimientoOrigen);
                        await _context.SaveChangesAsync();
                        _notificacionAPIService.Envia_Push(billeteraOrigen.Cliente.Usuario.DeviceId, "Recepcion de dinero", $"Ha recibido ${request.External.Amount} en su billetera");

                        return Ok(new { success = true, message = "Transferencia realizada con éxito", data = transferencia.Data, code = "" });
                    }
                    else
                    {
                        Log.Error(transferencia.Message, "Error VerificarTransferenciaPSP 1");
                        return StatusCode(500, new { success = false, message = transferencia.Message, data = "", code = "" });
                    }
                }
                else
                {
                    Log.Error(solicitud.Message, "Error VerificarTransferenciaPSP 2");
                    return StatusCode(500, new { success = false, message = solicitud.Message, data = "", code = "No success" });
                }      
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error VerificarTransferenciaPSP 3");
                return StatusCode(500, new { success = false, message = ex.Message, data = "", code = "" });
            }
        }
    }
}