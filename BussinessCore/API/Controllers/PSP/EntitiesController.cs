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
        //        // Validar usuario autenticado usando el método corregido
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

                var lookup = await _pspService.ValidateExternalAccountAsync(request.TextSearch);

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

                if (request.Transaction == null || string.IsNullOrEmpty(request.UserToken))
                {
                    return BadRequest(new TransactionWithUATResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "Transaction y UserToken son requeridos",
                        Success = false
                    });
                }

                // Llamar al servicio PSP
                var result = await _pspService.CreateTransactionAsync(request.Transaction, request.UserToken);

                var response = new TransactionWithUATResponseDTO
                {
                    Status = result.Success ? 200 : 400,
                    UAT = request.UAT,
                    Mensaje = result.Success ? "Transacción iniciada" : (result.Error ?? "Error al crear transacción"),
                    Success = result.Success,
                    TransactionId = result.TransactionId,
                    RawResponse = result.RawResponse
                };

                if (result.Success)
                {
                    Log.Information($"Transacción creada en PSP - TransactionId: {result.TransactionId}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"Error al crear transacción en PSP: {result.Error}");
                    return BadRequest(response);
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