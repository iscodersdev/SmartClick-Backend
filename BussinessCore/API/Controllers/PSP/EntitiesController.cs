using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BusinessCore.Services;
using DAL.DTOs.PSP;
using DAL.Data;
using Serilog;
using System.Linq;

namespace SmartClickCore.API.Controllers.PSP
{
    [ApiController]
    [Route("api/psp/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private readonly IPSPService _pspService;
        private readonly SmartClickContext _context;

        public EntitiesController(SmartClickContext context, IPSPService pspService)
        {
            _context = context;
            _pspService = pspService;
        }

        // Método corregido para buscar usuario por UAT
        private DAL.Models.Usuario TraeUsuarioUAT(string uat)
        {
            return _context.UAT.Where(u => u.Token == uat).Select(u => u.Cliente.Usuario).FirstOrDefault();
        }

        /// <summary>
        /// Registra una nueva entidad en el PSP
        /// </summary>
        [HttpPost("RegistrarEntidad")]
        public async Task<IActionResult> RegistrarEntidad([FromBody] RegistrarEntidadRequestDTO request)
        {
            try
            {
                // Validar usuario autenticado usando el método corregido
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new RegistrarEntidadResponseDTO 
                    { 
                        Status = 401, 
                        UAT = request.UAT, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Validar datos requeridos
                if (string.IsNullOrEmpty(request.TributaryIdentifier) || 
                    string.IsNullOrEmpty(request.Name) || 
                    string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new RegistrarEntidadResponseDTO 
                    { 
                        Status = 400, 
                        UAT = request.UAT, 
                        Mensaje = "Datos incompletos: se requiere CUIT, nombre y email",
                        Success = false
                    });
                }

                // Llamar al servicio PSP
                var response = await _pspService.RegistrarEntidadAsync(request);

                if (response.Success)
                {
                    Log.Information($"Entidad registrada exitosamente en PSP para usuario {usuario.UserName}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error al registrar entidad en PSP: {response.Mensaje}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en RegistrarEntidad");
                return StatusCode(500, new RegistrarEntidadResponseDTO 
                { 
                    Status = 500, 
                    UAT = request.UAT, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        /// <summary>
        /// Valida la configuración del PSP
        /// </summary>
        [HttpPost("ValidarConfiguracion")]
        public IActionResult ValidarConfiguracion([FromBody] PSPBaseResponseDTO request)
        {
            try
            {
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new PSPBaseResponseDTO 
                    { 
                        Status = 401, 
                        UAT = request.UAT, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                bool isValid = _pspService.ValidateConfiguration();

                return Ok(new PSPBaseResponseDTO 
                { 
                    Status = 200, 
                    UAT = request.UAT, 
                    Mensaje = isValid ? "Configuración PSP válida" : "Configuración PSP inválida",
                    Success = isValid
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en ValidarConfiguracion");
                return StatusCode(500, new PSPBaseResponseDTO 
                { 
                    Status = 500, 
                    UAT = request.UAT, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        /// <summary>
        /// Obtiene un token del PSP (para pruebas)
        /// </summary>
        [HttpPost("ObtenerToken")]
        public async Task<IActionResult> ObtenerToken([FromBody] PSPBaseResponseDTO request)
        {
            try
            {
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new PSPBaseResponseDTO 
                    { 
                        Status = 401, 
                        UAT = request.UAT, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                var tokenResponse = await _pspService.GetAccessTokenAsync();

                if (!string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    return Ok(new 
                    { 
                        Status = 200, 
                        UAT = request.UAT, 
                        Mensaje = "Token obtenido exitosamente",
                        Success = true,
                        Token = tokenResponse.access_token,
                        ExpiresIn = tokenResponse.expires_in
                    });
                }
                else
                {
                    return BadRequest(new PSPBaseResponseDTO 
                    { 
                        Status = 400, 
                        UAT = request.UAT, 
                        Mensaje = "Error al obtener token del PSP",
                        Success = false
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en ObtenerToken");
                return StatusCode(500, new PSPBaseResponseDTO 
                { 
                    Status = 500, 
                    UAT = request.UAT, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }
    }
}