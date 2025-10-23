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
using DAL.DTOs.API;

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

        // *** NUEVO MÉTODO HELPER: OBTENER O REFRESCAR TOKEN DEL USUARIO PSP ***
        /// <summary>
        /// Obtiene el UserToken del PSP para un usuario, ya sea desde la base de datos o solicitándolo al PSP
        /// </summary>
        private async Task<string> ObtenerUserTokenPSP(DAL.Models.Usuario usuario)
        {
            try
            {
                // 1. Buscar PSPAccount existente para el usuario
                var pspAccount = _context.Set<DAL.Models.PSPAccount>()
                    .Where(p => p.Usuario.Id == usuario.Id)
                    .FirstOrDefault();

                // 2. Si existe y tiene token válido (no expirado), usarlo
                if (pspAccount != null &&
                    !string.IsNullOrEmpty(pspAccount.EncryptedUserToken) &&
                    pspAccount.TokenExpiry.HasValue &&
                    pspAccount.TokenExpiry.Value > DateTime.UtcNow.AddMinutes(5)) // Buffer de 5 minutos
                {
                    try
                    {
                        string decryptedToken = common.Decrypt(pspAccount.EncryptedUserToken, "PSPToken");
                        Log.Information($"Token recuperado desde BD para usuario {usuario.UserName}");
                        return decryptedToken;
                    }
                    catch (Exception ex)
                    {
                        Log.Warning(ex, $"Error descifrando token almacenado para usuario {usuario.UserName}");
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
                        string decryptedPassword = common.Decrypt(pspAccount.EncryptedPassword, "PSPPassword");
                        Log.Information($"Intentando obtener token del PSP para usuario {pspAccount.UserName}");

                        var tokenResponse = await _pspService.GetAccessTokenUserAsync(pspAccount.UserName, decryptedPassword);

                        if (!string.IsNullOrEmpty(tokenResponse?.access_token))
                        {
                            // Guardar el nuevo token en la BD
                            try
                            {
                                pspAccount.EncryptedUserToken = common.Encrypt(tokenResponse.access_token, "PSPToken");
                                pspAccount.TokenExpiry = tokenResponse.expires_in > 0
                                    ? (DateTime?)DateTime.UtcNow.AddSeconds(tokenResponse.expires_in)
                                    : null;
                                pspAccount.UpdatedAt = DateTime.UtcNow;

                                _context.SaveChanges();

                                Log.Information($"Token del PSP obtenido y guardado para usuario {pspAccount.UserName}");
                                return tokenResponse.access_token;
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Error guardando token en BD para usuario {pspAccount.UserName}");
                                // Aunque no se guarde, devolvemos el token obtenido
                                return tokenResponse.access_token;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"Error obteniendo token del PSP para usuario {pspAccount.UserName}");
                    }
                }

                // OPCIÓN B: Si tenemos un email y password almacenado en Cliente (fallback)
                var cliente = usuario.Clientes;
                if (cliente != null && !string.IsNullOrEmpty(cliente.Password))
                {
                    Log.Information($"Fallback: Intentando obtener token del PSP usando datos del Cliente");

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
                        }

                        try
                        {
                            pspAccount.EncryptedUserToken = common.Encrypt(tokenResponse.access_token, "PSPToken");
                            pspAccount.TokenExpiry = tokenResponse.expires_in > 0
                                ? (DateTime?)DateTime.UtcNow.AddSeconds(tokenResponse.expires_in)
                                : null;
                            pspAccount.UpdatedAt = DateTime.UtcNow;

                            _context.SaveChanges();

                            Log.Information($"Token del PSP obtenido y guardado para usuario {usuario.UserName}");
                            return tokenResponse.access_token;
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, $"Error guardando token en BD para usuario {usuario.UserName}");
                            // Aunque no se guarde, devolvemos el token obtenido
                            return tokenResponse.access_token;
                        }
                    }
                }

                Log.Warning($"No se pudo obtener UserToken para usuario {usuario.UserName}");
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error en ObtenerUserTokenPSP para usuario {usuario?.UserName}");
                return null;
            }
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

                    // *** PASO 2: GUARDAR CREDENCIALES PSP AUTOMÁTICAMENTE (OPCIÓN 1) ***
                    try
                    {
                        // Buscar usuario local por email para asociar PSPAccount
                        var usuarioLocal = _context.Usuarios.FirstOrDefault(u => u.UserName == request.user.email || u.Email == request.user.email);

                        if (usuarioLocal != null)
                        {
                            // Verificar si ya existe PSPAccount para este usuario
                            var pspAccountExistente = _context.Set<DAL.Models.PSPAccount>()
                                .FirstOrDefault(p => p.Usuario.Id == usuarioLocal.Id);

                            if (pspAccountExistente == null)
                            {
                                // Crear nuevo PSPAccount
                                var nuevoPspAccount = new DAL.Models.PSPAccount
                                {
                                    Usuario = usuarioLocal,
                                    UserName = request.user.userName,
                                    EncryptedPassword = common.Encrypt(request.user.password, "PSPPassword"),
                                    PSPUserId = pspResponse.UserId?.ToString(),
                                    Status = "active",
                                    CreatedAt = DateTime.UtcNow
                                };

                                _context.Set<DAL.Models.PSPAccount>().Add(nuevoPspAccount);
                                _context.SaveChanges();

                                Log.Information($"✅ Credenciales PSP guardadas automáticamente para usuario {usuarioLocal.UserName}");
                            }
                            else
                            {
                                // Actualizar PSPAccount existente
                                pspAccountExistente.UserName = request.user.userName;
                                pspAccountExistente.EncryptedPassword = common.Encrypt(request.user.password, "PSPPassword");
                                pspAccountExistente.PSPUserId = pspResponse.UserId?.ToString();
                                pspAccountExistente.UpdatedAt = DateTime.UtcNow;

                                _context.SaveChanges();

                                Log.Information($"✅ Credenciales PSP actualizadas automáticamente para usuario {usuarioLocal.UserName}");
                            }
                        }
                        else
                        {
                            Log.Warning($"⚠️ No se encontró usuario local con email {request.user.email} para asociar PSPAccount. Se puede guardar manualmente con el endpoint GuardarCredencialesPSP.");
                        }
                    }
                    catch (Exception exCredenciales)
                    {
                        Log.Error(exCredenciales, $"❌ Error guardando credenciales PSP para usuario {request.user.userName}. El usuario PSP fue creado exitosamente pero las credenciales no se guardaron localmente.");
                        // No fallar el request completo, solo advertir
                    }
                    // *** FIN PASO 2 ***

                    var pspResponseToken = await _pspService.GetAccessTokenUserAsync(pspRequest.userName, pspRequest.password);
                    if (pspResponseToken != null && !string.IsNullOrEmpty(pspResponseToken.access_token))
                    {
                        // Actualizar el token en la BD después de obtenerlo
                        try
                        {
                            var pspAccountToUpdate = _context.Set<DAL.Models.PSPAccount>().FirstOrDefault(p => p.Usuario.UserName == request.user.email || p.Usuario.Email == request.user.email);
                            if (pspAccountToUpdate != null)
                            {
                                pspAccountToUpdate.EncryptedUserToken = common.Encrypt(pspResponseToken.access_token, "PSPToken");
                                pspAccountToUpdate.TokenExpiry = pspResponseToken.expires_in > 0
                                    ? (DateTime?)DateTime.UtcNow.AddSeconds(pspResponseToken.expires_in)
                                    : null;
                                _context.SaveChanges();
                                Log.Information($"Token actualizado en BD para usuario {request.user.userName}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Warning(ex, $"No se pudo guardar el token en BD para {request.user.userName}, pero se continúa la operación.");
                        }

                        Log.Information($"Token Valido");
                        var pspResponseSelf = await _pspService.SelfRegistrationAsync(request.entity, pspResponseToken.access_token);

                        if (pspResponseSelf.Success)
                        {
                            Log.Information($"SelfRegistration completado exitosamente - CUIT: {request.entity.tributaryIdentifier}");
                            response.Mensaje += " | " + (_pspService.IsTestMode()
                                ? "?? SIMULACIÓN: Entidad creada mediante SelfRegistration (modo prueba)"
                                : "Entidad creada exitosamente mediante SelfRegistration");
                            response.Identifier = pspResponseSelf.Identifier;
                            response.EntityId = pspResponseSelf.EntityId;

                            var pspResponseFiles = await _pspService.UploadFilesAsync(pspResponseSelf.Identifier, pspResponse.UserToken, request.files);

                            if (pspResponseFiles.Success)
                            {
                                Log.Information($"Archivos subidos exitosamente - Identifier: {pspResponseSelf.Identifier}, Archivos: {request.files.Count}");
                                response.Mensaje += " | " + (_pspService.IsTestMode()
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

        // *** NUEVO ENDPOINT C1: OBTENER DATOS DE CUENTA DEL USUARIO LOGUEADO ***
        /// <summary>
        /// C1: Consulta los datos de la cuenta del usuario logueado (Accounts/All/Get)
        /// </summary>
        [HttpPost("GetAccountData")]
        public async Task<IActionResult> GetAccountData([FromBody] PSPBaseResponseDTO request)
        {
            try
            {
                // Validar usuario autenticado
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new AccountsInfoWithUATResponseDTO
                    {
                        Status = 401,
                        UAT = request.UAT,
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Obtener UserToken del PSP
                var userToken = await ObtenerUserTokenPSP(usuario);
                if (string.IsNullOrEmpty(userToken))
                {
                    return BadRequest(new AccountsInfoWithUATResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "No se pudo obtener el token del usuario PSP. Verifique que las credenciales estén guardadas.",
                        Success = false
                    });
                }

                // Llamar al servicio PSP
                var pspResponse = await _pspService.GetAccountDataAsync(userToken);

                var response = new AccountsInfoWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = request.UAT,
                    Mensaje = pspResponse.Success ? "Datos de cuenta obtenidos exitosamente" : pspResponse.Error ?? pspResponse.Message,
                    Success = pspResponse.Success,
                    Accounts = pspResponse.Accounts
                };

                if (pspResponse.Success)
                {
                    Log.Information($"C1: Datos de cuenta obtenidos exitosamente para usuario {usuario.UserName}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"C1: Error al obtener datos de cuenta: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en GetAccountData (C1)");
                return StatusCode(500, new AccountsInfoWithUATResponseDTO
                {
                    Status = 500,
                    UAT = request.UAT,
                    Mensaje = "Error interno del servidor",
                    Success = false
                });
            }
        }

        // *** NUEVO ENDPOINT C7: OBTENER ENTIDAD POR IDENTIFICADOR TRIBUTARIO ***
        /// <summary>
        /// C7: Obtiene la entidad hija por su identificador tributario (Accounts/Children/Get)
        /// </summary>
        [HttpPost("GetEntityByTributaryId")]
        public async Task<IActionResult> GetEntityByTributaryId([FromBody] EntityStatusWithUATRequestDTO request)
        {
            try
            {
                // Validar usuario autenticado
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new EntityStatusWithUATResponseDTO
                    {
                        Status = 401,
                        UAT = request.UAT,
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Validar que tengamos el TributaryIdentifier
                if (string.IsNullOrEmpty(request.TributaryIdentifier))
                {
                    return BadRequest(new EntityStatusWithUATResponseDTO
                    {
                        Status = 400,
                        UAT = request.UAT,
                        Mensaje = "TributaryIdentifier es requerido",
                        Success = false
                    });
                }

                // Obtener token del sistema (no del usuario)
                var systemToken = await _pspService.GetAccessTokenAsync();
                if (string.IsNullOrEmpty(systemToken?.access_token))
                {
                    return BadRequest(new EntityStatusWithUATResponseDTO
                    {
                        Status = 500,
                        UAT = request.UAT,
                        Mensaje = "No se pudo obtener el token del sistema PSP",
                        Success = false
                    });
                }

                // Llamar al servicio PSP
                var pspResponse = await _pspService.GetEntityByTributaryIdAsync(request.TributaryIdentifier, systemToken.access_token);

                var response = new EntityStatusWithUATResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = request.UAT,
                    Mensaje = pspResponse.Success ? "Entidad obtenida exitosamente" : pspResponse.Error ?? "Error al obtener entidad",
                    Success = pspResponse.Success,
                    Data = pspResponse.Data,
                    RawResponse = pspResponse.RawResponse
                };

                if (pspResponse.Success)
                {
                    Log.Information($"C7: Entidad obtenida exitosamente para TributaryIdentifier {request.TributaryIdentifier}");
                    return Ok(response);
                }
                else
                {
                    Log.Warning($"C7: Error al obtener entidad: {pspResponse.Error}");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en GetEntityByTributaryId (C7)");
                return StatusCode(500, new EntityStatusWithUATResponseDTO
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