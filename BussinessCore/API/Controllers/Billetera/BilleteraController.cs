using BussinessCore.API.Filters;
using DAL.Data;
using DAL.DTOs.API;
using DAL.Models;
using DAL.Models.Core;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartClickCore.API.Controllers.Billetera
{
    [TypeFilter(typeof(ChequeaUatApiAttribute))]
    [ApiController]
    [Route("api/[controller]")]
    public class BilleteraController : BaseApiController
    {

        public BilleteraController(SmartClickContext context) : base(context)
        {

        }

        /// <summary>
        /// Crea una billetera para el usuario autenticado y le asigna el CUIL proporcionado
        /// </summary>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBilletera([FromBody] CreateBilleteraWithCUILRequestDTO request)
        {
            try
            {
                // El filtro ChequeaUatApiAttribute ya validó que el UAT existe
                Log.Information($"Creando billetera para UAT: {request.UAT}");
                
                // Buscar cliente directamente desde UAT (más eficiente)
                var cliente = TraeClienteUAT(request.UAT);
                if (cliente == null)
                {
                    // Si no hay cliente directo, buscar por usuario
                    var usuario = TraeUsuarioUAT(request.UAT);
                    if (usuario != null)
                    {
                        cliente = _context.Clientes.Where(c => c.Usuario.Id == usuario.Id).FirstOrDefault();
                    }
                }

                if (cliente == null)
                {
                    Log.Warning($"Cliente no encontrado para UAT: {request.UAT}");
                    return BadRequest(new CreateBilleteraWithCUILResponseDTO
                    {
                        Status = 404,
                        UAT = request.UAT,
                        Mensaje = "No se encontró cliente asociado al usuario",
                        Success = false
                    });
                }

                Log.Information($"Cliente encontrado: {cliente.Id}");

                // Validar datos requeridos
                if (string.IsNullOrEmpty(request.CUIL))
                {
                    return BadRequest(new CreateBilleteraWithCUILResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "CUIL es requerido",
                        Success = false
                    });
                }

                // Normalizar y validar formato de CUIL
                var cuilNormalizado = NormalizarCUIL(request.CUIL);
                if (string.IsNullOrEmpty(cuilNormalizado))
                {
                    return BadRequest(new CreateBilleteraWithCUILResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "Formato de CUIL inválido. Use formato: 20-12345678-9 o 20123456789",
                        Success = false
                    });
                }

                // Verificar que el cliente no tenga ya una billetera
                var billeteraExistente = _context.Billeteras.Where(b => b.Cliente.Id == cliente.Id).FirstOrDefault();
                if (billeteraExistente != null)
                {
                    return BadRequest(new CreateBilleteraWithCUILResponseDTO
                    {
                        Status = 409,
                        UAT = request.UAT,
                        Mensaje = "El cliente ya tiene una billetera asignada",
                        Success = false
                    });
                }

                // Verificar que el CUIL no esté en uso por otra persona (comparar normalizado)
                var cuilExistente = _context.Personas
                    .Where(p => cliente.Persona == null || p.Id != cliente.Persona.Id)
                    .AsEnumerable()  // Traer a memoria para usar funciones C#
                    .Where(p => NormalizarCUIL(p.Cuil) == cuilNormalizado)
                    .FirstOrDefault();
                
                if (cuilExistente != null)
                {
                    return BadRequest(new CreateBilleteraWithCUILResponseDTO
                    {
                        Status = 409,
                        UAT = request.UAT,
                        Mensaje = "El CUIL ya está asignado a otra persona",
                        Success = false
                    });
                }

                // Generar CVU único
                string cvuGenerado;
                string aliasGenerado;
                do
                {
                    cvuGenerado = GenerarCVU();
                    aliasGenerado = GenerarAlias(cliente.Persona?.Nombres, cliente.Persona?.Apellido);
                } while (_context.Billeteras.Any(b => b.CVU == cvuGenerado || b.AliasCVU == aliasGenerado));

                // Actualizar el CUIL de la persona (guardar con formato estándar con guiones)
                if (cliente.Persona != null)
                {
                    cliente.Persona.Cuil = FormatearCUIL(cuilNormalizado);
                    cliente.Persona.FechaActualizacion = DateTime.Now;
                    _context.Update(cliente.Persona);
                    Log.Information($"CUIL actualizado para persona {cliente.Persona.Id}: {cliente.Persona.Cuil}");
                }

                // Crear la nueva billetera con saldo 0
                var nuevaBilletera = new DAL.Models.Core.Billetera
                {
                    Cliente = cliente,
                    Saldo = 0, // Saldo inicial 0 como se solicita
                    CVU = cvuGenerado,
                    AliasCVU = aliasGenerado,
                    QRCobro = null,
                    // Inicializar listas navegacionales
                    Tarjetas = new List<Tarjeta>(),
                    Cuentas = new List<CuentaBancaria>(),
                    Servicios = new List<ServicioBilletera>(),
                    Movimientos = new List<MovimientoBilletera>(),
                    Contactos = new List<ContactosBilletera>()
                };

                await _context.Billeteras.AddAsync(nuevaBilletera);
                await _context.SaveChangesAsync();

                Log.Information($"Billetera creada exitosamente - ClienteId: {cliente.Id}, CVU: {cvuGenerado}, CUIL: {cliente.Persona?.Cuil}");

                var response = new CreateBilleteraWithCUILResponseDTO
                {
                    Status = 200,
                    UAT = request.UAT,
                    Mensaje = $"Billetera creada exitosamente para {cliente.Persona?.GetNombreCompleto() ?? cliente.RazonSocial}",
                    Success = true,
                    BilleteraId = nuevaBilletera.Id,
                    CVU = nuevaBilletera.CVU,
                    AliasCVU = nuevaBilletera.AliasCVU,
                    Saldo = nuevaBilletera.Saldo,
                    ClienteId = cliente.Id,
                    CUIL = cliente.Persona?.Cuil ?? FormatearCUIL(cuilNormalizado)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al crear billetera para UAT: {request?.UAT}");
                return StatusCode(500, new CreateBilleteraWithCUILResponseDTO
                {
                    Status = 500,
                    UAT = request?.UAT,
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        /// <summary>
        /// Genera un CVU único de 22 dígitos
        /// </summary>
        private string GenerarCVU()
        {
            // Formato CVU: 0000003100010017366771 (22 dígitos)
            // Los primeros 6 dígitos suelen ser el código de la entidad
            // Los siguientes 16 son el número de cuenta
            var random = new Random();
            var entidad = "000000"; // Código de entidad fijo
            var cuenta = random.Next(10000000, 99999999).ToString() + random.Next(10000000, 99999999).ToString();
            return entidad + cuenta.Substring(0, 16);
        }

        /// <summary>
        /// Genera un alias único basado en el nombre y apellido
        /// </summary>
        private string GenerarAlias(string nombres, string apellido)
        {
            if (string.IsNullOrEmpty(nombres) || string.IsNullOrEmpty(apellido))
            {
                var random = new Random();
                return $"usuario.{random.Next(1000, 9999)}.smartclick";
            }

            var nombreLimpio = nombres.Split(' ')[0].ToLower().Replace(" ", "");
            var apellidoLimpio = apellido.Split(' ')[0].ToLower().Replace(" ", "");
            var random2 = new Random();
            
            return $"{nombreLimpio}.{apellidoLimpio}.{random2.Next(100, 999)}";
        }

        /// <summary>
        /// Normaliza el CUIL removiendo guiones y validando el formato
        /// </summary>
        private string NormalizarCUIL(string cuil)
        {
            if (string.IsNullOrWhiteSpace(cuil))
                return null;

            // Remover guiones y espacios
            var cuilLimpio = cuil.Replace("-", "").Replace(" ", "").Trim();

            // Validar que solo contenga dígitos y tenga 11 caracteres
            if (cuilLimpio.Length != 11 || !cuilLimpio.All(char.IsDigit))
                return null;

            // Validación básica de CUIL (los primeros dos dígitos deben estar entre 20-27, 30-33)
            var prefijo = cuilLimpio.Substring(0, 2);
            var prefijoNum = int.Parse(prefijo);
            
            if (!((prefijoNum >= 20 && prefijoNum <= 27) || (prefijoNum >= 30 && prefijoNum <= 33)))
                return null;

            return cuilLimpio;
        }

        /// <summary>
        /// Formatea el CUIL con guiones en el formato estándar XX-XXXXXXXX-X
        /// </summary>
        private string FormatearCUIL(string cuilNormalizado)
        {
            if (string.IsNullOrWhiteSpace(cuilNormalizado) || cuilNormalizado.Length != 11)
                return cuilNormalizado;

            return $"{cuilNormalizado.Substring(0, 2)}-{cuilNormalizado.Substring(2, 8)}-{cuilNormalizado.Substring(10, 1)}";
        }

        [HttpPost("MediosPago")]
        public async Task<IActionResult> MediosPago([FromBody] RespuestaAPI consultaDTO)
        {
            try
            {
                var usuario = TraeUsuarioUAT(consultaDTO.UAT);
                var billetera = TraeBilletera(usuario);

                List<MedioPagoDTO> mediosPago = billetera.Tarjetas.Select(t => new MedioPagoDTO { Id = t.Id, Descripcion = t.Numero, TipoMedioPago = TipoMedioPago.TipoTarjeta, DetalleAdicional = t.Vencimiento }).ToList();
                mediosPago.AddRange(billetera.Cuentas.Where(c => !c.Terceros).Select(c => new MedioPagoDTO { Id = c.Id, Descripcion = c.CBU, TipoMedioPago = TipoMedioPago.TipoCuenta, DetalleAdicional = c.Titular }).ToList());
                mediosPago.Add(new MedioPagoDTO { Id = billetera.Id, Descripcion = "Mi Billetera", TipoMedioPago = TipoMedioPago.TipoBilletera, DetalleAdicional = billetera.CVU });
                return new JsonResult(new ListaMediosPagoDTO { Status = 200, UAT = consultaDTO.UAT, Mensaje = "Medios de pago enviados", MediosPago = mediosPago.OrderBy(m => m.TipoMedioPago).ToList() });
            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta de medios de pago - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaDTO.UAT, Mensaje = "Error en consulta de medios de pago" });
            }

        }

        [HttpPost("BilleterasAsociadas")]
        public async Task<IActionResult> BilleterasAsociadas([FromBody] RespuestaAPI consultaDTO)
        {
            try
            {
                var billetera = TraeBilletera(TraeUsuarioUAT(consultaDTO.UAT));

                var billeterasAsociadas = _context.Billeteras.Where(b => billetera.Contactos.Select(c => c.ClienteContacto.Id).Contains(b.Cliente.Id))
                    .Select(b => new BilleteraAsociadaDTO { Titular = b.Cliente.RazonSocial, CVU = b.CVU }).ToList();
                return new JsonResult(new ListaBilleterasDTO { Status = 200, UAT = consultaDTO.UAT, Mensaje = "Billeteras asociadas enviadas", Billeteras = billeterasAsociadas });

            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta de billeteras asociadas - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaDTO.UAT, Mensaje = "Error en consulta de billeteras asociadas" });
            }

        }

        [HttpPost("SaldoBilletera")]
        public async Task<IActionResult> SaldoBilletera([FromBody] RespuestaAPI consultaSaldoDTO)
        {
            try
            {
                var cliente = TraeClienteUAT(consultaSaldoDTO.UAT);
                var billetera = _context.Billeteras.Where(b => b.Cliente.Id == cliente.Id).FirstOrDefault();
                if (billetera==null)
                {
                    Log.Error($"Error no hay billetera asignada");
                    return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaSaldoDTO.UAT, Mensaje = "Error no hay billeterra asignada" });
                }
                return new JsonResult(new SaldoDTO { Status = 200, UAT = consultaSaldoDTO.UAT, Mensaje = "Saldo enviado", Saldo = billetera.Saldo });
            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta del saldo - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaSaldoDTO.UAT, Mensaje = "Error en consulta de saldo" });
            }

        }

        [HttpPost("CVUBilletera")]
        public async Task<IActionResult> CVUBilletera([FromBody] RespuestaAPI consultaDTO)
        {
            try
            {
                var usuario = TraeUsuarioUAT(consultaDTO.UAT);
                var billetera = _context.Billeteras.Where(b => b.Cliente.Usuario.Id == usuario.Id).FirstOrDefault();
                return new JsonResult(new CVUBilleteraDTO { Status = 200, UAT = consultaDTO.UAT, Mensaje = "CVU enviado", CVU = billetera.CVU, Alias = billetera.AliasCVU });
            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta del CVU - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaDTO.UAT, Mensaje = "Error en consulta de CVU" });
            }

        }

        [HttpPost("MovimientosBilletera")]
        public async Task<IActionResult> MovimientosBilletera([FromBody] RespuestaAPI consultaMovimientosDTO)
        {
            try
            {
                var usuario = TraeUsuarioUAT(consultaMovimientosDTO.UAT);
                var billetera = _context.Billeteras.Where(b => b.Cliente.Usuario.Id == usuario.Id).FirstOrDefault();
                var movimientos = billetera.Movimientos.Select(m => new MovimientoBilleteraDTO { Monto = m.Monto, TipoMovimiento = m.TipoMovimiento.Nombre, Fecha = m.Fecha }).ToList();
                movimientos.AddRange(billetera.Tarjetas.SelectMany(t => t.Movimientos).Select(m => new MovimientoBilleteraDTO { Monto = m.Monto, TipoMovimiento = m.TipoMovimiento.Nombre, Fecha = m.Fecha }).ToList());
                movimientos.AddRange(billetera.Cuentas.Where(c => !c.Terceros).SelectMany(c => c.Movimientos).Select(m => new MovimientoBilleteraDTO { Monto = m.Monto, TipoMovimiento = m.TipoMovimiento.Nombre, Fecha = m.Fecha }).ToList());
                return new JsonResult(new ListaMovimientoDTO { Status = 200, UAT = consultaMovimientosDTO.UAT, Mensaje = "Movimientos enviados", Movimientos = movimientos.OrderByDescending(m => m.Fecha).ToList() });
            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta de movimientos - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaMovimientosDTO.UAT, Mensaje = "Error en consulta de movimientos" });
            }

        }

        [HttpPost("TarjetasBilletera")]
        public async Task<IActionResult> TarjetasBilletera([FromBody] RespuestaAPI consultaTarjetasDTO)
        {
            try
            {
                var billetera = TraeBilletera(TraeUsuarioUAT(consultaTarjetasDTO.UAT));
                var tarjetas = billetera.Tarjetas.Select(t => new TarjetasBilleteraDTO { Titular = t.Titular, Numero = t.Numero, Vencimiento = t.Vencimiento }).ToList();
                return new JsonResult(new ListaTarjetasDTO { Status = 200, UAT = consultaTarjetasDTO.UAT, Mensaje = "Tarjetas enviadas", Tarjetas = tarjetas });
            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta de tarjetas - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaTarjetasDTO.UAT, Mensaje = "Error en consulta de tarjetas" });
            }

        }

        [HttpPost("CuentasBilletera")]
        public async Task<IActionResult> CuentasBilletera([FromBody] RespuestaAPI consultaDTO)
        {
            try
            {
                var billetera = TraeBilletera(TraeUsuarioUAT(consultaDTO.UAT));
                var cuentas = billetera.Cuentas.Where(c => !c.Terceros).Select(c => new CuentaBilleteraDTO { CBU = c.CBU, Descripcion = $"{c.Alias} {c.Titular}".Trim() }).ToList();
                return new JsonResult(new ListaCuentasDTO { Status = 200, UAT = consultaDTO.UAT, Mensaje = "Cuentas enviadas", Cuentas = cuentas });
            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta de tarjetas - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaDTO.UAT, Mensaje = "Error en consulta de cuentas" });
            }

        }

        [HttpPost("CuentasTercerosBilletera")]
        public async Task<IActionResult> CuentasTercerosBilletera([FromBody] RespuestaAPI consultaDTO)
        {
            try
            {
                var billetera = TraeBilletera(TraeUsuarioUAT(consultaDTO.UAT));
                var cuentas = billetera.Cuentas.Where(c => c.Terceros).Select(c => new CuentaBilleteraDTO { CBU = c.CBU, Descripcion = $"{c.Alias} {c.Titular}".Trim() }).ToList();
                return new JsonResult(new ListaCuentasDTO { Status = 200, UAT = consultaDTO.UAT, Mensaje = "Cuentas enviadas", Cuentas = cuentas });
            }
            catch (Exception e)
            {
                Log.Error($"Error en consulta de tarjetas - {e.Message}");
                return new JsonResult(new RespuestaAPI { Status = 500, UAT = consultaDTO.UAT, Mensaje = "Error en consulta de cuentas" });
            }

        }

        /// <summary>
        /// Endpoint de diagnóstico para verificar información del UAT
        /// </summary>
        [HttpPost("DiagnosticUAT")]
        public async Task<IActionResult> DiagnosticUAT([FromBody] RespuestaAPI request)
        {
            try
            {
                // Buscar información completa del UAT
                var uatInfo = _context.UAT
                    .Where(u => u.Token == request.UAT)
                    .Select(u => new {
                        UATId = u.Id,
                        Token = u.Token,
                        FechaHora = u.FechaHora,
                        Usuario = u.Usuario != null ? new {
                            u.Usuario.Id,
                            u.Usuario.UserName,
                            u.Usuario.Email
                        } : null,
                        Cliente = u.Cliente != null ? new {
                            u.Cliente.Id,
                            u.Cliente.RazonSocial,
                            Persona = u.Cliente.Persona != null ? new {
                                u.Cliente.Persona.Id,
                                u.Cliente.Persona.Nombres,
                                u.Cliente.Persona.Apellido,
                                u.Cliente.Persona.Cuil,
                                u.Cliente.Persona.NroDocumento
                            } : null
                        } : null,
                        Persona = u.Persona != null ? new {
                            u.Persona.Id,
                            u.Persona.Nombres,
                            u.Persona.Apellido,
                            u.Persona.Cuil
                        } : null
                    })
                    .FirstOrDefault();

                if (uatInfo == null)
                {
                    return BadRequest(new {
                        Status = 404,
                        UAT = request.UAT,
                        Mensaje = "UAT no encontrado",
                        Success = false
                    });
                }

                // Verificar si tiene billetera
                DAL.Models.Core.Billetera billetera = null;
                if (uatInfo.Cliente != null)
                {
                    billetera = _context.Billeteras.Where(b => b.Cliente.Id == uatInfo.Cliente.Id).FirstOrDefault();
                }
                else if (uatInfo.Usuario != null)
                {
                    billetera = _context.Billeteras.Where(b => b.Cliente.Usuario.Id == uatInfo.Usuario.Id).FirstOrDefault();
                }

                var diagnosticInfo = new {
                    Status = 200,
                    UAT = request.UAT,
                    Mensaje = "Información de diagnóstico UAT",
                    Success = true,
                    UATInfo = uatInfo,
                    TieneBilletera = billetera != null,
                    BilleteraInfo = billetera != null ? new {
                        billetera.Id,
                        billetera.CVU,
                        billetera.AliasCVU,
                        billetera.Saldo
                    } : null,
                    MetodosAcceso = new {
                        TraeUsuarioUAT = TraeUsuarioUAT(request.UAT) != null,
                        TraeClienteUAT = TraeClienteUAT(request.UAT) != null
                    }
                };

                Log.Information($"Diagnóstico UAT completado para: {request.UAT}");
                return Ok(diagnosticInfo);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error en diagnóstico UAT: {request?.UAT}");
                return StatusCode(500, new {
                    Status = 500,
                    UAT = request?.UAT,
                    Mensaje = "Error en diagnóstico UAT",
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Edita la billetera del usuario autenticado (solo billeteras propias inicialmente)
        /// </summary>
        [HttpPut("Edit")]
        public async Task<IActionResult> EditBilletera([FromBody] EditBilleteraRequestDTO request)
        {
            try
            {
                Log.Information($"Editando billetera para UAT: {request.UAT}, BilleteraId: {request.BilleteraId}");
                
                var cliente = TraeClienteUAT(request.UAT);
                var usuario = (cliente == null) ? TraeUsuarioUAT(request.UAT) : TraeUsuarioUAT(request.UAT);

                if (cliente == null)
                {
                    if (usuario != null)
                    {
                        cliente = _context.Clientes.Where(c => c.Usuario.Id == usuario.Id).FirstOrDefault();
                    }
                }

                if (cliente == null)
                {
                    return BadRequest(new EditBilleteraResponseDTO
                    {
                        Status = 404,
                        UAT = request.UAT,
                        Mensaje = "No se encontró cliente asociado al usuario",
                        Success = false
                    });
                }

                // Buscar billetera: si el cliente no quiere conocer el Id, buscamos por cliente.
                DAL.Models.Core.Billetera billetera = null;

                if (request.BilleteraId > 0)
                {
                    // Si envían BilleteraId, validamos que pertenezca al cliente
                    billetera = _context.Billeteras
                        .Where(b => b.Id == request.BilleteraId && b.Cliente != null && b.Cliente.Id == cliente.Id)
                        .FirstOrDefault();

                    if (billetera == null)
                    {
                        // Intentar fallback: tal vez el cliente envió el Id equivocado, usamos la billetera asociada al cliente
                        billetera = _context.Billeteras
                            .Where(b => b.Cliente != null && b.Cliente.Id == cliente.Id)
                            .FirstOrDefault();

                        if (billetera != null)
                        {
                            Log.Warning($"BilleteraId recibido ({request.BilleteraId}) no pertenece al ClienteId={cliente.Id}. Usando billetera Id={billetera.Id} asociada al cliente.");
                        }
                    }
                }
                else
                {
                    // No se envió BilleteraId: buscar la billetera asociada al cliente automáticamente
                    billetera = _context.Billeteras
                        .Where(b => b.Cliente != null && b.Cliente.Id == cliente.Id)
                        .FirstOrDefault();
                }

                if (billetera == null)
                {
                    return BadRequest(new EditBilleteraResponseDTO
                    {
                        Status = 404,
                        UAT = request.UAT,
                        Mensaje = "Billetera no encontrada o no pertenece al usuario",
                        Success = false
                    });
                }

                bool huboModificaciones = false;
                string mensajeModificaciones = "";

                // Validar y actualizar Alias CVU
                if (!string.IsNullOrWhiteSpace(request.AliasCVU) && request.AliasCVU != billetera.AliasCVU)
                {
                    var aliasExistente = _context.Billeteras
                        .Where(b => b.Id != billetera.Id && b.AliasCVU == request.AliasCVU.Trim())
                        .FirstOrDefault();
                    
                    if (aliasExistente != null)
                    {
                        return BadRequest(new EditBilleteraResponseDTO
                        {
                            Status = 409,
                            UAT = request.UAT,
                            Mensaje = "El alias CVU ya está en uso por otra billetera",
                            Success = false
                        });
                    }

                    var aliasAnterior = billetera.AliasCVU;
                    billetera.AliasCVU = request.AliasCVU.Trim();
                    huboModificaciones = true;
                    mensajeModificaciones += $"Alias actualizado de '{aliasAnterior}' a '{billetera.AliasCVU}'. ";
                }

                // Validar y actualizar CUIL
                if (!string.IsNullOrWhiteSpace(request.CUIL))
                {
                    var cuilNormalizado = NormalizarCUIL(request.CUIL);
                    if (string.IsNullOrEmpty(cuilNormalizado))
                    {
                        return BadRequest(new EditBilleteraResponseDTO
                        {
                            Status = 400,
                            UAT = request.UAT,
                            Mensaje = "Formato de CUIL inválido. Use formato: 20-12345678-9 o 20123456789",
                            Success = false
                        });
                    }

                    var cuilExistente = _context.Personas
                        .Where(p => cliente.Persona == null || p.Id != cliente.Persona.Id)
                        .AsEnumerable()
                        .Where(p => NormalizarCUIL(p.Cuil) == cuilNormalizado)
                        .FirstOrDefault();
                    
                    if (cuilExistente != null)
                    {
                        return BadRequest(new EditBilleteraResponseDTO
                        {
                            Status = 409,
                            UAT = request.UAT,
                            Mensaje = "El CUIL ya está asignado a otra persona",
                            Success = false
                        });
                    }

                    if (cliente.Persona != null)
                    {
                        var cuilFormateado = FormatearCUIL(cuilNormalizado);
                        if (cliente.Persona.Cuil != cuilFormateado)
                        {
                            var cuilAnterior = cliente.Persona.Cuil;
                            cliente.Persona.Cuil = cuilFormateado;
                            cliente.Persona.FechaActualizacion = DateTime.Now;
                            _context.Update(cliente.Persona);
                            huboModificaciones = true;
                            mensajeModificaciones += $"CUIL actualizado de '{cuilAnterior}' a '{cuilFormateado}'. ";
                        }
                    }
                }

                if (!huboModificaciones)
                {
                    return Ok(new EditBilleteraResponseDTO
                    {
                        Status = 200,
                        UAT = request.UAT,
                        Mensaje = "No se realizaron modificaciones (los datos son iguales a los actuales)",
                        Success = true,
                        BilleteraId = billetera.Id,
                        CVU = billetera.CVU,
                        AliasCVU = billetera.AliasCVU,
                        Saldo = billetera.Saldo,
                        ClienteId = cliente.Id,
                        CUIL = cliente.Persona?.Cuil
                    });
                }

                // Guardar cambios
                _context.Update(billetera);
                await _context.SaveChangesAsync();

                return Ok(new EditBilleteraResponseDTO
                {
                    Status = 200,
                    UAT = request.UAT,
                    Mensaje = $"Billetera editada exitosamente. {mensajeModificaciones.Trim()}",
                    Success = true,
                    BilleteraId = billetera.Id,
                    CVU = billetera.CVU,
                    AliasCVU = billetera.AliasCVU,
                    Saldo = billetera.Saldo,
                    ClienteId = cliente.Id,
                    CUIL = cliente.Persona?.Cuil
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al editar billetera para UAT: {request?.UAT}");
                return StatusCode(500, new EditBilleteraResponseDTO
                {
                    Status = 500,
                    UAT = request?.UAT,
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }
    }
}
