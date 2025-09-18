using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BusinessCore.Services;
using DAL.DTOs.PSP;
using DAL.Data;
using Serilog;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using DAL.Models.Core; // for MovimientoBilletera, OrigenMovimiento, TipoOrigenMovimiento
using Commons.Extensions; // for GetDisplayName extension
using Microsoft.EntityFrameworkCore;

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

        // *** NUEVO ENDPOINT: CREAR USUARIO ***
        /// <summary>
        /// Crea un nuevo usuario en el PSP
        /// </summary>
        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] CreateUserEntidadRequestDTO request)
        {
            try
            {
                // Validar usuario administrador autenticado
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new CreateUserWithUATResponseDTO 
                    { 
                        Status = 401, 
                        UAT = request.UAT, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Validar datos requeridos para crear usuario
                if (string.IsNullOrEmpty(request.user.userName) || 
                    string.IsNullOrEmpty(request.user.email) || 
                    string.IsNullOrEmpty(request.user.password))
                {
                    return BadRequest(new CreateUserWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = request.UAT, 
                        Mensaje = "Datos incompletos: se requiere userName, email y password",
                        Success = false
                    });
                }

                // Convertir request con UAT al request del servicio PSP
                var pspRequest = new CreateUserRequestDTO
                {
                    userType = request.user.userType,
                    userName = request.user.userName,
                    documentType = request.user.documentType,
                    documentNumber = request.user.documentNumber,
                    firstName = request.user.firstName,
                    lastName = request.user.lastName,
                    email = request.user.email,
                    phoneNumber = request.user.phoneNumber,
                    address = request.user.address,
                    departmentId = request.user.departmentId,
                    cityId = request.user.cityId,
                    Active = request.user.Active,
                    roles = request.user.roles,
                    password = request.user.password,
                    passwordConfirm = request.user.passwordConfirm
                };


                // Llamar al servicio PSP
                var pspResponse = await _pspService.CreateUserAsync(request.user);


                var mensaje = _pspService.IsTestMode() 
                    ? "?? SIMULACIÓN: Usuario creado (modo prueba)"
                    : "Usuario creado exitosamente";

                                      
                
                var response = new CreateUserWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = request.UAT,
                    Mensaje = pspResponse.Success ? mensaje : "Error al crear usuario",
                    Success = pspResponse.Success,
                    UserId = pspResponse.UserId,
                    UserToken = pspResponse.UserToken
                };

                if (pspResponse.Success)
                {
                    Log.Information($"Usuario creado exitosamente en PSP - UserName: {request.user.userName}");

                    var pspResponseToken = await _pspService.GetAccessTokenUserAsync(pspRequest.userName, pspRequest.password);
                    if (pspResponseToken != null && !string.IsNullOrEmpty(pspResponseToken.access_token))
                    {
                        Log.Information($"Token Valido");
                        var pspResponseSelf = await _pspService.SelfRegistrationAsync(request.entity, pspResponseToken.access_token);

                        if (pspResponseSelf.Success)
                        {
                            Log.Information($"SelfRegistration completado exitosamente - CUIT: {request.entity.tributaryIdentifier}");
                            response.Mensaje += " | " + ( _pspService.IsTestMode() 
                                ? "?? SIMULACIÓN: Entidad creada mediante SelfRegistration (modo prueba)"
                                : "Entidad creada exitosamente mediante SelfRegistration");
                            response.Identifier = pspResponseSelf.Identifier;
                            response.EntityId = pspResponseSelf.EntityId;

                            var pspResponseFiles = await _pspService.UploadFilesAsync(pspResponseSelf.Identifier, pspResponse.UserToken, request.files);

                            if (pspResponseFiles.Success)
                            {
                                Log.Information($"Archivos subidos exitosamente - Identifier: {pspResponseSelf.Identifier}, Archivos: {request.files.Count}");
                                response.Mensaje += " | " + ( _pspService.IsTestMode() 
                                    ? "?? SIMULACIÓN: Archivos subidos exitosamente (modo prueba)"
                                    : "Archivos subidos exitosamente");
                            }
                            else
                            {
                                Log.Warning($"Error al subir archivos: {pspResponseFiles.Error}");
                                response.Mensaje += " | Error al subir archivos: " + pspResponseFiles.Error;
                            }



                        }
                        else
                        {
                            Log.Warning($"Error en SelfRegistration: {pspResponseSelf.Error}");
                            response.Mensaje += " | Error en SelfRegistration: " + pspResponseSelf.Error;
                        }
                    }
                    else
                    {
                        Log.Warning($"Token Invalido");
                    }
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error al crear usuario en PSP: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en CrearUsuario");
                return StatusCode(500, new CreateUserWithUATResponseDTO 
                { 
                    Status = 500, 
                    UAT = request.UAT, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        // *** NUEVO ENDPOINT: SELF REGISTRATION ***
        /// <summary>
        /// Crea una entidad asociada al usuario autenticado (SelfRegistration)
        /// </summary>
        [HttpPost("SelfRegistration")]
        public async Task<IActionResult> SelfRegistration([FromBody] SelfRegistrationWithUATRequestDTO request)
        {
            try
            {
                // Validar usuario autenticado
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new SelfRegistrationWithUATResponseDTO 
                    { 
                        Status = 401, 
                        UAT = request.UAT, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Validar datos requeridos
                if (string.IsNullOrEmpty(request.tributaryIdentifier) || 
                    string.IsNullOrEmpty(request.name) || 
                    string.IsNullOrEmpty(request.email))
                {
                    return BadRequest(new SelfRegistrationWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = request.UAT, 
                        Mensaje = "Datos incompletos: se requiere CUIT, nombre y email",
                        Success = false
                    });
                }

                // Validar que tenemos el token del usuario para SelfRegistration
                if (string.IsNullOrEmpty(request.UserToken))
                {
                    return BadRequest(new SelfRegistrationWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = request.UAT, 
                        Mensaje = "Se requiere UserToken del usuario creado previamente",
                        Success = false
                    });
                }

                // Convertir request con UAT al request del servicio PSP
                var pspRequest = new SelfRegistrationRequestDTO
                {
                    entityTypeId = request.entityTypeId,
                    parentId = request.parentId,
                    isPhysicalPerson = request.isPhysicalPerson,
                    taxPayer = request.taxPayer,
                    isPyME = request.isPyME,
                    PyMEEffectiveDate = request.PyMEEffectiveDate,
                    tributaryIdentifierType = request.tributaryIdentifierType,
                    tributaryIdentifier = request.tributaryIdentifier,
                    name = request.name,
                    phoneCode = request.phoneCode,
                    phone = request.phone,
                    address = request.address,
                    floor = request.floor,
                    department = request.department,
                    cityId = request.cityId,
                    postalCode = request.postalCode,
                    email = request.email,
                    isRevalidation = request.isRevalidation,
                    IsSameAddress = request.IsSameAddress,
                    activityPostalCode = request.activityPostalCode,
                    activityCityId = request.activityCityId,
                    activityAddress = request.activityAddress,
                    activityFloor = request.activityFloor,
                    activityDepartment = request.activityDepartment,
                    FantasyName = request.FantasyName,
                    cuf = request.cuf,
                    CovenantCode = request.CovenantCode
                };

                // Llamar al servicio PSP con el token del usuario
                var pspResponse = await _pspService.SelfRegistrationAsync(pspRequest, request.UserToken);

                var mensaje = _pspService.IsTestMode() 
                    ? "?? SIMULACIÓN: Entidad creada mediante SelfRegistration (modo prueba)"
                    : "Entidad creada exitosamente mediante SelfRegistration";

                var response = new SelfRegistrationWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = request.UAT,
                    Mensaje = pspResponse.Success ? mensaje : "Error en SelfRegistration",
                    Success = pspResponse.Success,
                    Identifier = pspResponse.Identifier,
                    EntityId = pspResponse.EntityId
                };

                if (pspResponse.Success)
                {
                    Log.Information($"SelfRegistration completado exitosamente - CUIT: {request.tributaryIdentifier}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error en SelfRegistration: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en SelfRegistration");
                return StatusCode(500, new SelfRegistrationWithUATResponseDTO 
                { 
                    Status = 500, 
                    UAT = request.UAT, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        // *** NUEVO ENDPOINT: UPLOAD FILES ***
        /// <summary>
        /// Sube archivos de validación (DNI, selfie, inscripción AFIP, etc.)
        /// </summary>
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles([FromQuery] string identifier, [FromQuery] string userToken, [FromQuery] string uat)
        {
            try
            {
                // Validar usuario administrador autenticado
                var usuario = TraeUsuarioUAT(uat);
                if (usuario == null)
                {
                    return BadRequest(new UploadFilesWithUATResponseDTO 
                    { 
                        Status = 401, 
                        UAT = uat, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Validar parámetros requeridos
                if (string.IsNullOrEmpty(identifier))
                {
                    return BadRequest(new UploadFilesWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = uat, 
                        Mensaje = "Identifier requerido",
                        Success = false
                    });
                }

                if (string.IsNullOrEmpty(userToken))
                {
                    return BadRequest(new UploadFilesWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = uat, 
                        Mensaje = "UserToken requerido",
                        Success = false
                    });
                }

                // Validar que tengamos archivos
                if (Request.Form.Files == null || Request.Form.Files.Count == 0)
                {
                    return BadRequest(new UploadFilesWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = uat, 
                        Mensaje = "Al menos un archivo es requerido",
                        Success = false
                    });
                }

                // Convertir IFormFile a Dictionary<string, byte[]>
                var files = new Dictionary<string, byte[]>();
                foreach (var file in Request.Form.Files)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        files.Add(file.Name, stream.ToArray());
                    }
                }

                // Llamar al servicio PSP
                var pspResponse = await _pspService.UploadFilesAsync(identifier, userToken, files);

                var mensaje = _pspService.IsTestMode() 
                    ? "?? SIMULACIÓN: Archivos subidos exitosamente (modo prueba)"
                    : "Archivos subidos exitosamente";

                var response = new UploadFilesWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = uat,
                    Mensaje = pspResponse.Success ? mensaje : "Error al subir archivos",
                    Success = pspResponse.Success,
                    UploadedFiles = pspResponse.UploadedFiles
                };

                if (pspResponse.Success)
                {
                    Log.Information($"Archivos subidos exitosamente - Identifier: {identifier}, Archivos: {files.Count}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error al subir archivos: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en UploadFiles");
                return StatusCode(500, new UploadFilesWithUATResponseDTO 
                { 
                    Status = 500, 
                    UAT = uat, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        // *** NUEVO ENDPOINT: OBTENER PROVINCIAS ***
        /// <summary>
        /// Obtiene la lista de provincias disponibles
        /// </summary>
        [HttpGet("Provinces")]
        public async Task<IActionResult> GetProvinces([FromQuery] string uat)
        {
            try
            {
                // Validar usuario autenticado
                var usuario = TraeUsuarioUAT(uat);
                if (usuario == null)
                {
                    return BadRequest(new ProvincesWithUATResponseDTO 
                    { 
                        Status = 401, 
                        UAT = uat, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Llamar al servicio PSP
                var pspResponse = await _pspService.GetProvincesAsync();

                var mensaje = _pspService.IsTestMode() 
                    ? "?? SIMULACIÓN: Provincias obtenidas (modo prueba)"
                    : "Provincias obtenidas exitosamente";

                var response = new ProvincesWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = uat,
                    Mensaje = pspResponse.Success ? mensaje : "Error al obtener provincias",
                    Success = pspResponse.Success,
                    Provinces = pspResponse.Provinces
                };

                if (pspResponse.Success)
                {
                    Log.Information($"Provincias obtenidas exitosamente - Total: {pspResponse.Provinces.Count}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error al obtener provincias: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en GetProvinces");
                return StatusCode(500, new ProvincesWithUATResponseDTO 
                { 
                    Status = 500, 
                    UAT = uat, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        // *** NUEVO ENDPOINT: OBTENER CIUDADES ***
        /// <summary>
        /// Obtiene la lista de ciudades de una provincia específica
        /// </summary>
        [HttpGet("Cities")]
        public async Task<IActionResult> GetCities([FromQuery] int provinceId, [FromQuery] string uat)
        {
            try
            {
                // Validar usuario autenticado
                var usuario = TraeUsuarioUAT(uat);
                if (usuario == null)
                {
                    return BadRequest(new CitiesWithUATResponseDTO 
                    { 
                        Status = 401, 
                        UAT = uat, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Validar parámetro
                if (provinceId <= 0)
                {
                    return BadRequest(new CitiesWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = uat, 
                        Mensaje = "ProvinceId debe ser mayor a 0",
                        Success = false
                    });
                }

                // Llamar al servicio PSP
                var pspResponse = await _pspService.GetCitiesAsync(provinceId);

                var mensaje = _pspService.IsTestMode() 
                    ? $"?? SIMULACIÓN: Ciudades obtenidas para provincia {provinceId} (modo prueba)"
                    : $"Ciudades obtenidas exitosamente para provincia {provinceId}";

                var response = new CitiesWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = uat,
                    Mensaje = pspResponse.Success ? mensaje : "Error al obtener ciudades",
                    Success = pspResponse.Success,
                    Cities = pspResponse.Cities
                };

                if (pspResponse.Success)
                {
                    Log.Information($"Ciudades obtenidas exitosamente - Provincia: {provinceId}, Total: {pspResponse.Cities.Count}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error al obtener ciudades: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en GetCities");
                return StatusCode(500, new CitiesWithUATResponseDTO 
                { 
                    Status = 500, 
                    UAT = uat, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        // *** NUEVO ENDPOINT: CREAR ENTIDAD Y USUARIO EN UNA SOLA OPERACIÓN ***
        /// <summary>
        /// Crea una entidad y usuario en el PSP en una sola operación (endpoint /Entities/Persons/New)
        /// </summary>
        //[HttpPost("CrearEntidadYUsuario")]
        //public async Task<IActionResult> CrearEntidadYUsuario([FromBody] CreateEntityAndUserWithUATRequestDTO request)
        //{
        //    try
        //    {
        //        // Validar usuario administrador autenticado
        //        var usuario = TraeUsuarioUAT(request.UAT);
        //        if (usuario == null)
        //        {
        //            return BadRequest(new CreateEntityAndUserWithUATResponseDTO 
        //            { 
        //                Status = 401, 
        //                UAT = request.UAT, 
        //                Mensaje = "Usuario no autenticado",
        //                Success = false
        //            });
        //        }

        //        // Validar datos requeridos de la entidad
        //        if (string.IsNullOrEmpty(request.entity.tributaryIdentifier) || 
        //            string.IsNullOrEmpty(request.entity.name) || 
        //            string.IsNullOrEmpty(request.entity.email))
        //        {
        //            return BadRequest(new CreateEntityAndUserWithUATResponseDTO 
        //            { 
        //                Status = 400, 
        //                UAT = request.UAT, 
        //                Mensaje = "Datos de entidad incompletos: se requiere CUIT, nombre y email",
        //                Success = false
        //            });
        //        }

        //        // Validar datos requeridos de la persona/usuario
        //        if (string.IsNullOrEmpty(request.person.userName) || 
        //            string.IsNullOrEmpty(request.person.email) || 
        //            string.IsNullOrEmpty(request.person.documentNumber))
        //        {
        //            return BadRequest(new CreateEntityAndUserWithUATResponseDTO 
        //            { 
        //                Status = 400, 
        //                UAT = request.UAT, 
        //                Mensaje = "Datos de usuario incompletos: se requiere userName, email y documentNumber",
        //                Success = false
        //            });
        //        }

        //        // Convertir request con UAT al request del servicio PSP
        //        var pspRequest = new CreateEntityUserRequestDTO
        //        {
        //            entity = request.entity,
        //            person = request.person
        //        };

        //        // Llamar al servicio PSP (REAL - sin mock)
        //        var pspResponse = await _pspService.CreateEntityAndUserAsync(pspRequest);

        //        var response = new CreateEntityAndUserWithUATResponseDTO
        //        {
        //            Status = pspResponse.Success ? 200 : 500,
        //            UAT = request.UAT,
        //            Mensaje = pspResponse.Success ? "Entidad y usuario creados exitosamente" : pspResponse.Error ?? "Error al crear entidad y usuario",
        //            Success = pspResponse.Success,
        //            EntityId = pspResponse.EntityId,
        //            PersonId = pspResponse.PersonId
        //        };

        //        if (pspResponse.Success)
        //        {
        //            Log.Information($"Entidad y usuario creados exitosamente - CUIT: {request.entity.tributaryIdentifier}, UserName: {request.person.userName}");
        //            return Ok(response);
        //        }
        //        else
        //        {
        //            Log.Warning($"Error al crear entidad y usuario: {pspResponse.Error}");
        //            return BadRequest(response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Error en CrearEntidadYUsuario");
        //        return StatusCode(500, new CreateEntityAndUserWithUATResponseDTO 
        //        { 
        //            Status = 500, 
        //            UAT = request.UAT, 
        //            Mensaje = "Error interno del servidor",
        //            Success = false
        //        });
        //    }
        //}

        /// <summary>
        /// Registra una nueva entidad en el PSP
        /// </summary>
        /// 
        //[HttpPost("RegistrarEntidad")]
        //public async Task<IActionResult> RegistrarEntidad([FromBody] RegistrarEntidadRequestDTO request)
        //{
        //    try
        //    {
        //        // Validar usuario autenticado usando el método corrigido
        //        var usuario = TraeUsuarioUAT(request.UAT);
        //        if (usuario == null)
        //        {
        //            return BadRequest(new RegistrarEntidadResponseDTO 
        //            { 
        //                Status = 401, 
        //                UAT = request.UAT, 
        //                Mensaje = "Usuario no autenticado",
        //                Success = false
        //            });
        //        }

        //        // Validar datos requeridos
        //        if (string.IsNullOrEmpty(request.TributaryIdentifier) || 
        //            string.IsNullOrEmpty(request.Name) || 
        //            string.IsNullOrEmpty(request.Email))
        //        {
        //            return BadRequest(new RegistrarEntidadResponseDTO 
        //            { 
        //                Status = 400, 
        //                UAT = request.UAT, 
        //                Mensaje = "Datos incompletos: se requiere CUIT, nombre y email",
        //                Success = false
        //            });
        //        }

        //        // Llamar al servicio PSP
        //        var response = await _pspService.RegistrarEntidadAsync(request);

        //        if (response.Success)
        //        {
        //            Log.Information($"Entidad registrada exitosamente en PSP para usuario {usuario.UserName}");
        //            return Ok(response);
        //        }
        //        else
        //        {
        //            Log.Warning($"Error al registrar entidad en PSP: {response.Mensaje}");
        //            return BadRequest(response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Error en RegistrarEntidad");
        //        return StatusCode(500, new RegistrarEntidadResponseDTO 
        //        { 
        //            Status = 500, 
        //            UAT = request.UAT, 
        //            Mensaje = "Error interno del servidor",
        //            Success = false
        //        });
        //    }
        //}

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

        // *** NUEVO ENDPOINT: OBTENER INFORMACIÓN DE CUENTAS ***
        /// <summary>
        /// Obtiene la información de las cuentas del usuario logueado
        /// </summary>
        [HttpGet("AccountsInfo")]
        public async Task<IActionResult> GetAccountsInfo([FromQuery] string userToken, [FromQuery] string uat)
        {
            try
            {
                // Validar usuario administrador autenticado por UAT
                var usuario = TraeUsuarioUAT(uat);
                if (usuario == null)
                {
                    return BadRequest(new AccountsInfoWithUATResponseDTO 
                    { 
                        Status = 401, 
                        UAT = uat, 
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Validar que tenemos el token del usuario para consultar sus cuentas
                if (string.IsNullOrEmpty(userToken))
                {
                    return BadRequest(new AccountsInfoWithUATResponseDTO 
                    { 
                        Status = 400, 
                        UAT = uat, 
                        Mensaje = "UserToken requerido para consultar las cuentas",
                        Success = false
                    });
                }

                // Llamar al servicio PSP con el token del usuario
                var pspResponse = await _pspService.GetAccountsInfoAsync(userToken);

                var response = new AccountsInfoWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = uat,
                    Mensaje = pspResponse.Success ? "Información de cuentas obtenida exitosamente" : "Error al obtener información de cuentas",
                    Success = pspResponse.Success,
                    Accounts = pspResponse.Accounts
                };

                if (pspResponse.Success)
                {
                    Log.Information($"Información de cuentas obtenida exitosamente - Total cuentas: {pspResponse.Accounts.Count}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error al obtener información de cuentas: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en GetAccountsInfo");
                return StatusCode(500, new AccountsInfoWithUATResponseDTO 
                { 
                    Status = 500, 
                    UAT = uat, 
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        /// <summary>
        /// Valida una cuenta externa (alias/CVU/CBU) usando PSP
        /// </summary>
        [HttpPost("ValidateExternalAccount")]
        public async Task<IActionResult> ValidateExternalAccount([FromBody] ValidateExternalAccountRequestDTO request)
        {
            try
            {
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new ExternalAccountWithUATResponseDTO
                    {
                        Status = 401,
                        UAT = request.UAT,
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                if (string.IsNullOrEmpty(request.TextSearch))
                {
                    return BadRequest(new ExternalAccountWithUATResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "TextSearch requerido",
                        Success = false
                    });
                }

                // Use UserToken if provided, otherwise fallback to system token inside service
                var lookup = await _pspService.ValidateExternalAccountAsync(request.TextSearch, request.UserToken);

                if (lookup != null && lookup.success)
                {
                    return Ok(new ExternalAccountWithUATResponseDTO
                    {
                        Status = 200,
                        UAT = request.UAT,
                        Mensaje = "Cuenta externa validada",
                        Success = true,
                        Data = lookup.data
                    });
                }
                else
                {
                    return BadRequest(new ExternalAccountWithUATResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = lookup?.message ?? "Error al validar cuenta externa",
                        Success = false
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en ValidateExternalAccount");
                return StatusCode(500, new ExternalAccountWithUATResponseDTO
                {
                    Status = 500,
                    UAT = request.UAT,
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        // *** NUEVO ENDPOINT: CREAR TRANSACCIÓN ***
        /// <summary>
        /// Crea una transacción en el PSP (puede ser externa). Usa UAT para validar usuario administrador.
        /// </summary>
        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionWithUATRequestDTO request)
        {
            try
            {
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new TransactionWithUATResponseDTO
                    {
                        Status = 401,
                        UAT = request.UAT,
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                if (request.Transaction == null)
                {
                    return BadRequest(new TransactionWithUATResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "Transaction es requerida",
                        Success = false
                    });
                }

                var destAccountNumber = request.Transaction.destinationAccount?.accountNumber;
                if (string.IsNullOrEmpty(destAccountNumber))
                {
                    return BadRequest(new TransactionWithUATResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "Número de cuenta destino requerido",
                        Success = false
                    });
                }

                // AUTOMÁTICO: Detectar si es interna o externa
                var billeteraDestino = _context.Billeteras.Where(b => b.CVU == destAccountNumber).FirstOrDefault();
                bool isInternalTransfer = billeteraDestino != null;

                Log.Information($"Detectando tipo de transferencia - CVU: {destAccountNumber}, Es interna: {isInternalTransfer}");

                if (isInternalTransfer)
                {
                    // *** TRANSFERENCIA INTERNA AUTOMÁTICA ***
                    Log.Information("Procesando transferencia INTERNA");

                    var uatEntryInterno = _context.UAT
                        .Include(u => u.Cliente)
                            .ThenInclude(c => c.Persona)
                        .Include(u => u.Cliente)
                            .ThenInclude(c => c.Usuario)
                                .ThenInclude(us => us.Personas)
                        .FirstOrDefault(u => u.Token == request.UAT);

                    var clienteOrigen = uatEntryInterno?.Cliente;

                    if (clienteOrigen == null)
                    {
                        return BadRequest(new TransactionWithUATResponseDTO
                        {
                            Status = 400,
                            UAT = request.UAT,
                            Mensaje = "No se encontró cliente asociado al UAT",
                            Success = false
                        });
                    }

                    var billeteraOrigen = _context.Billeteras.Where(b => b.Cliente.Id == clienteOrigen.Id).FirstOrDefault();
                    if (billeteraOrigen == null)
                    {
                        return BadRequest(new TransactionWithUATResponseDTO
                        {
                            Status = 400,
                            UAT = request.UAT,
                            Mensaje = "No se encontró billetera de origen",
                            Success = false
                        });
                    }

                    decimal monto;
                    try
                    {
                        monto = Convert.ToDecimal(request.Transaction.balance);
                    }
                    catch
                    {
                        return BadRequest(new TransactionWithUATResponseDTO
                        {
                            Status = 400,
                            UAT = request.UAT,
                            Mensaje = "Monto inválido",
                            Success = false
                        });
                    }

                    if (!billeteraOrigen.ChequeaDebito(monto))
                    {
                        return BadRequest(new TransactionWithUATResponseDTO
                        {
                            Status = 400,
                            UAT = request.UAT,
                            Mensaje = "El monto supera el saldo disponible",
                            Success = false
                        });
                    }

                    // Crear movimientos internos
                    var movimientoDestino = new MovimientoBilletera
                    {
                        CBU = billeteraOrigen.CVU,
                        Fecha = DateTime.Now,
                        Monto = monto,
                        OrigenAsociado = new OrigenMovimiento
                        {
                            TipoOrigen = TipoOrigenMovimiento.Billetera,
                            IdAsociado = billeteraOrigen.Id,
                            Descripcion = TipoOrigenMovimiento.Billetera.GetDisplayName()
                        },
                        TipoMovimiento = _context.TipoMovimientoBilletera.Find((int)TipoMovimientoBilleteraEnum.IngresoDinero)
                    };

                    billeteraDestino.Saldo += monto;
                    billeteraDestino.Movimientos.Add(movimientoDestino);
                    billeteraDestino.Contactos.Add(new ContactosBilletera
                    {
                        ClienteContacto = billeteraOrigen.Cliente,
                        Detalle = billeteraOrigen.Cliente.Usuario.Personas?.GetNombreCompleto()
                    });

                    var movimientoOrigen = new MovimientoBilletera
                    {
                        CBU = billeteraDestino.CVU,
                        Fecha = DateTime.Now,
                        Monto = monto,
                        OrigenAsociado = new OrigenMovimiento
                        {
                            TipoOrigen = TipoOrigenMovimiento.Billetera,
                            IdAsociado = billeteraDestino.Id,
                            Descripcion = TipoOrigenMovimiento.Billetera.GetDisplayName()
                        },
                        TipoMovimiento = _context.TipoMovimientoBilletera.Find((int)TipoMovimientoBilleteraEnum.EnvioBilletera)
                    };

                    billeteraOrigen.Saldo -= monto;
                    billeteraOrigen.Movimientos.Add(movimientoOrigen);
                    billeteraOrigen.Contactos.Add(new ContactosBilletera
                    {
                        ClienteContacto = billeteraDestino.Cliente,
                        Detalle = billeteraDestino.Cliente.Usuario.Personas?.GetNombreCompleto()
                    });

                    _context.Update(billeteraDestino);
                    _context.Update(billeteraOrigen);
                    _context.SaveChanges();

                    Log.Information($"Transferencia INTERNA completada: ${monto} de {billeteraOrigen.CVU} a {billeteraDestino.CVU}");

                    return Ok(new TransactionWithUATResponseDTO
                    {
                        Status = 200,
                        UAT = request.UAT,
                        Mensaje = $"Transferencia interna realizada exitosamente: ${monto}",
                        Success = true,
                        TransactionId = null // Las internas no tienen ID de PSP
                    });
                }
                else
                {
                    // *** TRANSFERENCIA EXTERNA AUTOMÁTICA ***
                    Log.Information("Procesando transferencia EXTERNA");

                    // Forzar isExternal = true para el PSP
                    request.Transaction.isExternal = true;

                    // *** OBTENER TOKEN DEL SISTEMA PSP UNA SOLA VEZ ***
                    Log.Debug("Antes de GetAccessTokenAsync");
                    var tokenResponse = await _pspService.GetAccessTokenAsync();
                    Log.Debug("Después de GetAccessTokenAsync - token? {TokenExists}", !string.IsNullOrEmpty(tokenResponse?.access_token));

                    if (string.IsNullOrEmpty(tokenResponse.access_token))
                    {
                        return BadRequest(new TransactionWithUATResponseDTO
                        {
                            Status = 400,
                            UAT = request.UAT,
                            Mensaje = "No se pudo obtener token de la cuenta recaudadora PSP",
                            Success = false
                        });
                    }

                    // *** VARIABLE ÚNICA PARA TODO EL FLUJO ***
                    string systemToken = tokenResponse.access_token;
                    Log.Information($"Token del sistema PSP obtenido: {systemToken.Substring(0, Math.Min(20, systemToken.Length))}...");

                    // *** VALIDACIÓN DE TITULARIDAD Y CUENTA EXTERNA ***
                    string localCuil = null;
                    try
                    {
                        // 1. Obtener el CUIL local correctamente usando Include
                        var uatEntry = _context.UAT
                            .Include(u => u.Cliente.Persona) // Cargar Cliente y luego Persona
                            .FirstOrDefault(u => u.Token == request.UAT);

                        var persona = uatEntry?.Cliente?.Persona;
                        localCuil = persona?.Cuil;

                        Log.Debug($"CUIL local obtenido: {localCuil}");

                        // 2. Validar cuenta externa en PSP USANDO EL MISMO TOKEN DEL SISTEMA
                        Log.Debug("Antes de ValidateExternalAccountAsync con systemToken");
                        var lookup = await _pspService.ValidateExternalAccountAsync(destAccountNumber, systemToken);
                        //                                                                              ↑ AHORA PASA EL MISMO TOKEN
                        Log.Debug("Después de ValidateExternalAccountAsync - success? {Success}", lookup?.success);

                        if (lookup == null || !lookup.success || lookup.data == null)
                        {
                            return BadRequest(new TransactionWithUATResponseDTO
                            {
                                Status = 400,
                                UAT = request.UAT,
                                Mensaje = "Cuenta externa no encontrada o inválida",
                                Success = false
                            });
                        }

                        // 3. Comparar titularidad si tenemos el CUIL local
                        if (!string.IsNullOrEmpty(localCuil))
                        {
                            var normLocal = new string(localCuil.Where(char.IsDigit).ToArray());
                            var extTrib = lookup.data.tributaryIdentifier ?? string.Empty;
                            var normExt = new string(extTrib.Where(char.IsDigit).ToArray());

                            Log.Information($"Validando titularidad - Local: {normLocal}, Externo: {normExt}");

                            if (!string.Equals(normLocal, normExt, StringComparison.OrdinalIgnoreCase))
                            {
                                Log.Warning($"Titularidad no coincide - Usuario local CUIL: {localCuil}, Cuenta externa CUIT: {extTrib}");
                                return BadRequest(new TransactionWithUATResponseDTO
                                {
                                    Status = 400,
                                    UAT = request.UAT,
                                    Mensaje = $"La cuenta externa no pertenece al mismo titular. Local: {localCuil} vs Externo: {extTrib}",
                                    Success = false
                                });
                            }
                            Log.Information($"Validación de titularidad exitosa - CUIL coincide: {localCuil}");
                        }
                        else
                        {
                            Log.Warning("Usuario local sin CUIL registrado - no se puede validar titularidad antes de la operación.");
                        }

                        Log.Information($"Cuenta externa validada - Titular: {lookup.data.tributaryIdentifier}, Tipo: {lookup.data.accountTypeDescription}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error validando cuenta externa o titularidad");
                        return StatusCode(500, new TransactionWithUATResponseDTO
                        {
                            Status = 500,
                            UAT = request.UAT,
                            Mensaje = "Error validando cuenta externa o titularidad",
                            Success = false
                        });
                    }

                    // Verificar saldo local antes de enviar al PSP
                    var clienteOrigen = _context.UAT.Where(u => u.Token == request.UAT).Select(u => u.Cliente).FirstOrDefault();
                    if (clienteOrigen != null)
                    {
                        var billeteraOrigen = _context.Billeteras.Where(b => b.Cliente.Id == clienteOrigen.Id).FirstOrDefault();
                        if (billeteraOrigen != null)
                        {
                            if (decimal.TryParse(request.Transaction.balance.ToString(), out decimal monto))
                            {
                                if (!billeteraOrigen.ChequeaDebito(monto))
                                {
                                    return BadRequest(new TransactionWithUATResponseDTO
                                    {
                                        Status = 400,
                                        UAT = request.UAT,
                                        Mensaje = "Saldo insuficiente para transferencia externa",
                                        Success = false
                                    });
                                }
                            }
                        }
                    }

                    // 4. Llamar al PSP para crear la transacción externa USANDO EL MISMO TOKEN
                    Log.Debug("Antes de CreateTransactionAsync hacia PSP con CUIL local: {cuil}", localCuil);

                    // *** COMPLETAR CAMPOS OBLIGATORIOS AUTOMÁTICAMENTE ***
                    // 1. Agregar currencyTypeId si no está presente
                    if (string.IsNullOrEmpty(request.Transaction.currencyTypeId) || request.Transaction.currencyTypeId == "0")
                    {
                        request.Transaction.currencyTypeId = "1"; // Pesos Argentinos por defecto
                        Log.Information("Agregando currencyTypeId por defecto: 1 (Pesos)");
                    }

                    // 2. Completar originAccount si está vacío
                    if (request.Transaction.originAccount == null || string.IsNullOrEmpty(request.Transaction.originAccount.accountNumber))
                    {
                        // Obtener información de cuentas del usuario para completar originAccount
                        var accountsInfo = await _pspService.GetAccountsInfoAsync(systemToken);
                        if (accountsInfo.Success && accountsInfo.Accounts != null && accountsInfo.Accounts.Any())
                        {
                            var firstAccount = accountsInfo.Accounts.First();
                            request.Transaction.originAccount = new AccountRefDTO // ← CAMBIO: OriginAccountDTO → AccountRefDTO
                            {
                                accountNumber = firstAccount.accountNumber,
                                accountTypeId = firstAccount.accountTypeId,
                                tributaryIdentifierType = firstAccount.tributaryIdentifierType ?? "CUIT",
                                tributaryIdentifier = firstAccount.tributaryIdentifier ?? ""
                            };
                            
                            Log.Information($"Completando originAccount automáticamente - CVU: {firstAccount.accountNumber}");
                        }
                        else
                        {
                            Log.Warning("No se pudo obtener información de cuentas del usuario para completar originAccount");
                            return BadRequest(new TransactionWithUATResponseDTO
                            {
                                Status = 400,
                                UAT = request.UAT,
                                Mensaje = "No se pudo obtener información de la cuenta origen del usuario",
                                Success = false
                            });
                        }
                    }

                    // 3. Completar availabilityDate si no está presente
                    if (string.IsNullOrEmpty(request.Transaction.availabilityDate)) // ← CAMBIO: == default(DateTime) → string.IsNullOrEmpty
                    {
                        request.Transaction.availabilityDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // ← CAMBIO: DateTime → string
                        Log.Information($"Agregando availabilityDate automática: {request.Transaction.availabilityDate}");
                    }

                    // 4. Completar transactionTypeId si no está presente
                    if (request.Transaction.transactionTypeId == 0)
                    {
                        request.Transaction.transactionTypeId = 1; // Débito por defecto
                        Log.Information("Agregando transactionTypeId por defecto: 1 (Débito)");
                    }

                    // 5. Completar concept si no está presente
                    if (string.IsNullOrEmpty(request.Transaction.concept))
                    {
                        request.Transaction.concept = "VAR"; // Varios por defecto
                        Log.Information("Agregando concept por defecto: VAR (Varios)");
                    }

                    var result = await _pspService.CreateTransactionAsync(request.Transaction, systemToken, localCuil);
                    //                                                                        ↑ MISMO TOKEN USADO AQUÍ TAMBIÉN
                    Log.Debug("Después de CreateTransactionAsync - result.Success: {Success}", result?.Success);

                    // Si PSP creó la transacción exitosamente, debitar saldo local
                    if (result.Success)
                    {
                        try
                        {
                            if (clienteOrigen != null)
                            {
                                var billeteraOrigen = _context.Billeteras.Where(b => b.Cliente.Id == clienteOrigen.Id).FirstOrDefault();
                                if (billeteraOrigen != null)
                                {
                                    if (decimal.TryParse(request.Transaction.balance.ToString(), out decimal monto))
                                    {
                                        // Registrar movimiento de débito local por transferencia externa
                                        var movimiento = new MovimientoBilletera
                                        {
                                            CBU = destAccountNumber,
                                            Fecha = DateTime.Now,
                                            Monto = monto,
                                            OrigenAsociado = new OrigenMovimiento
                                            {
                                                TipoOrigen = TipoOrigenMovimiento.Cuenta,
                                                IdAsociado = 0,
                                                Descripcion = "Transferencia Externa PSP"
                                            },
                                            TipoMovimiento = _context.TipoMovimientoBilletera.FirstOrDefault(t => t.Id == (int)TipoMovimientoBilleteraEnum.EnvioBilletera)
                                        };

                                        billeteraOrigen.Saldo -= monto;
                                        billeteraOrigen.Movimientos.Add(movimiento);
                                        _context.Update(billeteraOrigen);
                                        Log.Debug("Antes de _context.SaveChanges");
                                        _context.SaveChanges();
                                        Log.Debug("Después de _context.SaveChanges");

                                        Log.Information($"Transferencia EXTERNA completada: ${monto} a {destAccountNumber}, PSP TransactionId: {result.TransactionId}");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Warning(ex, "No se pudo registrar débito local tras transferencia externa exitosa");
                        }
                    }

                    var response = new TransactionWithUATResponseDTO
                    {
                        Status = result.Success ? 200 : 400,
                        UAT = request.UAT,
                        Mensaje = result.Success ? $"Transferencia externa realizada exitosamente: ${request.Transaction.balance}" : (result.Error ?? "Error al crear transacción externa"),
                        Success = result.Success,
                        TransactionId = result.TransactionId,
                        RawResponse = result.RawResponse
                    };

                    if (result.Success)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        Log.Warning($"Error en transferencia externa: {result.Error}");
                        return BadRequest(response);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en CreateTransaction");
                return StatusCode(500, new TransactionWithUATResponseDTO
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