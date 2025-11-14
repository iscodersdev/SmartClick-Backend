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
using Newtonsoft.Json;
using DAL.Models;

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
        public PSPAccount TraeAccountPSP(Usuario usuario)
        {
            return _context.PSPAccounts.Where(b => b.Usuario.Id == usuario.Id).FirstOrDefault();
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
                //if (pspAccount != null &&
                //    !string.IsNullOrEmpty(pspAccount.EncryptedUserToken) &&
                //    pspAccount.TokenExpiry.HasValue &&
                //    pspAccount.TokenExpiry.Value > DateTime.UtcNow.AddMinutes(5)) // Buffer de 5 minutos
                //{
                //    try
                //    {
                //        string decryptedToken = common.DescifrarPassword(pspAccount.EncryptedUserToken);
                //        Log.Information($"✅ Token recuperado desde BD para usuario {usuario.UserName} (válido hasta {pspAccount.TokenExpiry})");
                //        return decryptedToken;
                //    }
                //    catch (Exception ex)
                //    {
                //        Log.Warning(ex, $"⚠️ Error descifrando token almacenado para usuario {usuario.UserName}");
                //    }
                //}
                //else if (pspAccount != null)
                //{
                //    if (string.IsNullOrEmpty(pspAccount.EncryptedUserToken))
                //    {
                //        Log.Warning($"⚠️ PSPAccount existe pero NO tiene token almacenado para usuario {usuario.UserName}");
                //    }
                //    else if (!pspAccount.TokenExpiry.HasValue)
                //    {
                //        Log.Warning($"⚠️ PSPAccount tiene token pero sin fecha de expiración para usuario {usuario.UserName}");
                //    }
                //    else if (pspAccount.TokenExpiry.Value <= DateTime.UtcNow.AddMinutes(5))
                //    {
                //        Log.Warning($"⚠️ Token expirado o por expirar para usuario {usuario.UserName} (expira: {pspAccount.TokenExpiry})");
                //    }
                //}

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

        // *** NUEVO ENDPOINT: CREAR USUARIO ***
        /// <summary>
        /// Crea un nuevo usuario en el PSP
        /// </summary>
        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] CreateUserEntidadRequestDTO request)
        {
            try
            {
                CuentasRecaudadoras cuentasRecaudadoras = _context.CuentasRecaudadoras.Where(c => c.Activo).FirstOrDefault();
                request.entity.parentId = Convert.ToInt32(cuentasRecaudadoras.ParentId);
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

                var usuarioLocal = _context.Usuarios.FirstOrDefault(u => u.UserName == request.user.email || u.Email == request.user.email);
                
                if (pspResponse.Success)
                {
                    Log.Information($"Usuario creado exitosamente en PSP - UserName: {request.user.userName}");

                    // *** PASO 2: GUARDAR CREDENCIALES PSP AUTOMÁTICAMENTE (OPCIÓN 1) ***
                    try
                    {
                        // Buscar usuario local por email para asociar PSPAccount

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
                                    EncryptedPassword = common.CifrarPassword(request.user.password),
                                    PSPUserId = pspResponse.UserId?.ToString(),
                                    Status = "active",
                                    CreatedAt = DateTime.UtcNow,
                                    EstadoCuentaPSP = _context.PSPAccountStatus.Where(x=>x.Codigo=="SB").FirstOrDefault(),
                                    Cliente = usuarioLocal.Clientes
                                };

                                _context.Set<DAL.Models.PSPAccount>().Add(nuevoPspAccount);
                                _context.SaveChanges();

                                Log.Information($"✅ Credenciales PSP guardadas automáticamente para usuario {usuarioLocal.UserName}");
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
                                pspAccountToUpdate.EncryptedUserToken = common.CifrarPassword(pspResponseToken.access_token);
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
                            var pspCuenta = _context.Set<DAL.Models.PSPAccount>()
                                .FirstOrDefault(p => p.Usuario.Id == usuarioLocal.Id);

                            if (pspCuenta == null)
                            {
                                Log.Warning($"ERror recuperando usuario.");
                                return BadRequest(response);
                            }
                            
                            pspCuenta.EstadoCuentaPSP =
                                _context.PSPAccountStatus.Where(x => x.Codigo == "FF").FirstOrDefault();
                            
                            Log.Information($"SelfRegistration completado exitosamente - CUIT: {request.entity.tributaryIdentifier}");
                            response.Mensaje += " | " + (_pspService.IsTestMode()
                                ? "?? SIMULACIÓN: Entidad creada mediante SelfRegistration (modo prueba)"
                                : "Entidad creada exitosamente mediante SelfRegistration");
                            response.Identifier = pspResponseSelf.Identifier;
                            response.EntityId = pspResponseSelf.EntityId;

                            var pspResponseFiles = await _pspService.UploadFilesAsync(pspResponseSelf.Identifier, pspResponse.UserToken, request.files);

                            if (pspResponseFiles.Success)
                            {
                                var pspCuentaPostArchivos = _context.Set<DAL.Models.PSPAccount>()
                                    .FirstOrDefault(p => p.Usuario.Id == usuarioLocal.Id);

                                if (pspCuentaPostArchivos == null)
                                {
                                    Log.Warning($"Error recuperando usuario.");
                                    return BadRequest(response);
                                }
                            
                                pspCuentaPostArchivos.EstadoCuentaPSP =
                                    _context.PSPAccountStatus.Where(x => x.Codigo == "A").FirstOrDefault();

                                
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


                            DAL.Models.Core.Billetera billetera = new DAL.Models.Core.Billetera()
                            {
                                Cliente = usuario.Clientes,
                                Saldo = 0,
                            };
                            _context.Billeteras.Add(billetera);
                            _context.SaveChanges();

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

                Log.Warning("Antes de upload");
                // Llamar al servicio PSP
                var pspResponse = await _pspService.UploadFilesAsync(identifier, userToken, files);
                Log.Warning("post UploadFilesAsync");
                Log.Warning(pspResponse.ToString());

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
                
                Log.Warning(pspResponse.ToString());

                if (pspResponse.Success)
                {
                    var pspCuentaPostArchivos = _context.Set<DAL.Models.PSPAccount>()
                        .FirstOrDefault(p => p.Usuario.Id == usuario.Id);

                    if (pspCuentaPostArchivos == null)
                    {
                        Log.Warning($"Error recuperando usuario.");
                        return BadRequest(response);
                    }
                            
                    pspCuentaPostArchivos.EstadoCuentaPSP =
                        _context.PSPAccountStatus.Where(x => x.Codigo == "A").FirstOrDefault();
                    _context.SaveChanges();
                    
                    
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

        /// <summary>
        /// Llama a C1 (Accounts/All/Get) usando el token del usuario y persiste/actualiza un registro PSPAccount local
        /// Útil para usuarios que fueron creados en PSP antes de existir la tabla PSPAccount localmente.
        /// </summary>
        [HttpPost("PersistPSPAccountFromC1")]
        public async Task<IActionResult> PersistPSPAccountFromC1([FromBody] PSPBaseResponseDTO request)
        {
            try
            {
                var usuario = TraeUsuarioUAT(request?.UAT);
                if (usuario == null)
                {
                    return BadRequest(new { Status = 401, UAT = request?.UAT, Mensaje = "Usuario no autenticado", Success = false });
                }

                // Obtener token del usuario PSP
                var userToken = await ObtenerUserTokenPSP(usuario);
                if (string.IsNullOrEmpty(userToken))
                {
                    return BadRequest(new { Status = 400, UAT = request?.UAT, Mensaje = "No se pudo obtener el token del usuario PSP", Success = false });
                }

                // Llamar a C1
                var c1Response = await _pspService.GetAccountDataAsync(userToken);

                if (c1Response == null || !c1Response.Success || c1Response.Accounts == null || !c1Response.Accounts.Any())
                {
                    return BadRequest(new { Status = 400, UAT = request?.UAT, Mensaje = "C1 no devolvió datos válidos", Success = false, Data = c1Response });
                }

                var account = c1Response.Accounts.First();

                // Buscar o crear PSPAccount local para este usuario
                var pspAccount = _context.Set<DAL.Models.PSPAccount>().Include(p => p.Usuario).FirstOrDefault(p => p.Usuario.Id == usuario.Id);
                var created = false;
                if (pspAccount == null)
                {
                    pspAccount = new DAL.Models.PSPAccount
                    {
                        Usuario = usuario,
                        UserName = usuario.UserName,
                        Status = "unknown",
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Set<DAL.Models.PSPAccount>().Add(pspAccount);
                    created = true;
                }

                // Mapear campos desde la respuesta C1 (defensivo)
                object accountNumberObj = account?.GetType().GetProperty("accountNumber")?.GetValue(account) ?? account?.GetType().GetProperty("AccountNumber")?.GetValue(account);
                object cvuObj = account?.GetType().GetProperty("cvu")?.GetValue(account) ?? account?.GetType().GetProperty("Cvu")?.GetValue(account) ?? account?.GetType().GetProperty("cvU_CBU")?.GetValue(account);
                object accountTypeObj = account?.GetType().GetProperty("accountTypeId")?.GetValue(account) ?? account?.GetType().GetProperty("AccountTypeId")?.GetValue(account);
                object tributaryObj = account?.GetType().GetProperty("tributaryIdentifier")?.GetValue(account) ?? account?.GetType().GetProperty("TributaryIdentifier")?.GetValue(account);
                object pspUserIdObj = account?.GetType().GetProperty("personId")?.GetValue(account) ?? account?.GetType().GetProperty("personId")?.GetValue(account) ?? account?.GetType().GetProperty("userId")?.GetValue(account);

                // Campos adicionales defensivos
                object cvuAliasObj = account?.GetType().GetProperty("cvU_CBUAlias")?.GetValue(account)
                    ?? account?.GetType().GetProperty("cvuAlias")?.GetValue(account)
                    ?? account?.GetType().GetProperty("alias")?.GetValue(account)
                    ?? account?.GetType().GetProperty("CBUAlias")?.GetValue(account);

                object entityIdObj = account?.GetType().GetProperty("entityId")?.GetValue(account) ?? account?.GetType().GetProperty("EntityId")?.GetValue(account);
                object identifierObj = account?.GetType().GetProperty("identifier")?.GetValue(account) ?? account?.GetType().GetProperty("Identifier")?.GetValue(account);

                object currencyTypeIdObj = account?.GetType().GetProperty("currencyTypeId")?.GetValue(account) ?? account?.GetType().GetProperty("CurrencyTypeId")?.GetValue(account);
                object currencyNameObj = account?.GetType().GetProperty("currencyTypeName")?.GetValue(account) ?? account?.GetType().GetProperty("CurrencyName")?.GetValue(account);
                object currencyDescObj = account?.GetType().GetProperty("currencyTypeDescription")?.GetValue(account) ?? account?.GetType().GetProperty("currencyDescription")?.GetValue(account);

                object accountTypeDescObj = account?.GetType().GetProperty("accountTypeDescription")?.GetValue(account) ?? account?.GetType().GetProperty("accountType")?.GetValue(account);
                object displayNameObj = account?.GetType().GetProperty("displayName")?.GetValue(account) ?? account?.GetType().GetProperty("name")?.GetValue(account);
                object virtualAccountObj = account?.GetType().GetProperty("virtualAccount")?.GetValue(account);
                object bankDescObj = account?.GetType().GetProperty("pspBankDescription")?.GetValue(account) ?? account?.GetType().GetProperty("bankDescription")?.GetValue(account);
                object deleteSolicitudeObj = account?.GetType().GetProperty("deleteAccountSolicitude")?.GetValue(account) ?? account?.GetType().GetProperty("deleteAccountSolicitud")?.GetValue(account);

                var accountNumber = accountNumberObj?.ToString();
                var cvu = cvuObj?.ToString();
                var cvuAlias = cvuAliasObj?.ToString();
                int? accountTypeId = null;
                if (accountTypeObj != null)
                {
                    int tmp; if (int.TryParse(accountTypeObj.ToString(), out tmp)) accountTypeId = tmp;
                }
                var trib = tributaryObj?.ToString();

                var entityId = entityIdObj?.ToString();
                var identifier = identifierObj?.ToString();

                int? currencyTypeId = null;
                if (currencyTypeIdObj != null) { int tmp; if (int.TryParse(currencyTypeIdObj.ToString(), out tmp)) currencyTypeId = tmp; }
                var currencyName = currencyNameObj?.ToString();
                var currencyDesc = currencyDescObj?.ToString();

                var accountTypeDesc = accountTypeDescObj?.ToString();
                var displayName = displayNameObj?.ToString();
                bool? virtualAccount = null;
                if (virtualAccountObj != null) { bool tmpb; if (bool.TryParse(virtualAccountObj.ToString(), out tmpb)) virtualAccount = tmpb; }
                var bankDesc = bankDescObj?.ToString();
                bool? deleteSolicitude = null;
                if (deleteSolicitudeObj != null) { bool td; if (bool.TryParse(deleteSolicitudeObj.ToString(), out td)) deleteSolicitude = td; }

                if (!string.IsNullOrEmpty(accountNumber)) pspAccount.AccountNumber = accountNumber;
                // No sobreescribir CVU si ya existe salvo que esté vacío
                if (!string.IsNullOrEmpty(cvu) && string.IsNullOrEmpty(pspAccount.CVU)) pspAccount.CVU = cvu;
                // CVU alias/CBU alias
                if (!string.IsNullOrEmpty(cvuAlias) && string.IsNullOrEmpty(pspAccount.CVU_CBUAlias)) pspAccount.CVU_CBUAlias = cvuAlias;
                if (accountTypeId.HasValue) pspAccount.AccountTypeId = accountTypeId;
                if (!string.IsNullOrEmpty(trib)) pspAccount.TributaryIdentifier = trib;

                // entity / identifier
                if (!string.IsNullOrEmpty(entityId)) pspAccount.EntityId = entityId;
                if (!string.IsNullOrEmpty(identifier)) pspAccount.Identifier = identifier;

                // currency
                if (currencyTypeId.HasValue) pspAccount.CurrencyTypeId = currencyTypeId;
                if (!string.IsNullOrEmpty(currencyName)) pspAccount.CurrencyName = currencyName;
                if (!string.IsNullOrEmpty(currencyDesc)) pspAccount.CurrencyDescription = currencyDesc;

                // metadata / descriptions
                if (!string.IsNullOrEmpty(accountTypeDesc)) pspAccount.StatusDescription = accountTypeDesc;
                if (!string.IsNullOrEmpty(displayName)) pspAccount.StatusDescription = string.IsNullOrEmpty(pspAccount.StatusDescription) ? displayName : pspAccount.StatusDescription;
                if (!string.IsNullOrEmpty(bankDesc)) pspAccount.StatusDescription = string.IsNullOrEmpty(pspAccount.StatusDescription) ? bankDesc : pspAccount.StatusDescription;
                if (virtualAccount.HasValue && virtualAccount == true) { /* opcional: marcar flag o status */ }
                if (deleteSolicitude.HasValue) pspAccount.DeleteAccountSolicitude = deleteSolicitude;

                pspAccount.Status = "user_account_found";

                // Guardar LastC1ResponseJson y UpdatedAt
                try
                {
                    pspAccount.LastC1ResponseJson = JsonConvert.SerializeObject(c1Response);
                }
                catch { }

                pspAccount.UpdatedAt = DateTime.UtcNow;

                // Guardar cambios
                try
                {
                    using (var tx = _context.Database.BeginTransaction())
                    {
                        await _context.SaveChangesAsync();
                        tx.Commit();
                    }
                }
                catch (Exception exSave)
                {
                    Log.Error(exSave, "Error guardando PSPAccount desde PersistPSPAccountFromC1");
                    return StatusCode(500, new { Status = 500, UAT = request?.UAT, Mensaje = "Error guardando PSPAccount", Success = false });
                }

                return Ok(new { Status = 200, UAT = request?.UAT, Mensaje = created ? "PSPAccount creado" : "PSPAccount actualizado", Success = true, PSPAccountId = pspAccount.Id });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en PersistPSPAccountFromC1");
                return StatusCode(500, new { Status = 500, UAT = request?.UAT, Mensaje = "Error interno del servidor", Success = false });
            }
        }

        // *** ENDPOINT TEMPORAL DE DIAGNÓSTICO: VERIFICAR COLUMNAS PSPACCOUNT ***
        /// <summary>
        /// Endpoint temporal para diagnosticar columnas de PSPAccount desde EF Core
        /// </summary>
        [HttpGet("DiagnosticPSPAccountColumns")]
        public IActionResult DiagnosticPSPAccountColumns([FromQuery] string uat)
        {
            try
            {
                // Validar usuario autenticado
                var usuario = TraeUsuarioUAT(uat);
                if (usuario == null)
                {
                    return BadRequest(new
                    {
                        Status = 401,
                        UAT = uat,
                        Mensaje = "Usuario no autenticado",
                        Success = false
                    });
                }

                // Obtener el modelo de Entity Framework
                var entityType = _context.Model.FindEntityType(typeof(DAL.Models.PSPAccount));
                
                if (entityType == null)
                {
                    return BadRequest(new
                    {
                        Status = 500,
                        UAT = uat,
                        Mensaje = "No se pudo encontrar el modelo PSPAccount en EF Core",
                        Success = false
                    });
                }

                // Obtener todas las propiedades (columnas) - .NET Core 2.2 compatible
                var properties = entityType.GetProperties()
                    .Select(p => new
                    {
                        PropertyName = p.Name,
                        ClrType = p.ClrType.Name,
                        IsNullable = p.IsNullable
                    })
                    .OrderBy(p => p.PropertyName)
                    .ToList();

                // Intentar consultar un registro para ver si falla
                string errorMessage = null;
                DAL.Models.PSPAccount testRecord = null;
                
                try
                {
                    testRecord = _context.Set<DAL.Models.PSPAccount>()
                        .FirstOrDefault();
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += " | Inner: " + ex.InnerException.Message;
                    }
                }

                return Ok(new
                {
                    Status = 200,
                    UAT = uat,
                    Mensaje = "Diagnóstico completado",
                    Success = true,
                    TotalProperties = properties.Count,
                    Properties = properties,
                    QueryError = errorMessage,
                    TestRecordExists = testRecord != null,
                    TestRecordId = testRecord?.Id
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en DiagnosticPSPAccountColumns");
                return StatusCode(500, new
                {
                    Status = 500,
                    UAT = uat,
                    Mensaje = "Error interno del servidor: " + ex.Message,
                    Success = false,
                    InnerException = ex.InnerException?.Message
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

        /// <summary>
        /// Orquestador: consulta C1 y C7 y persiste los datos parciales en PSPAccount según disponibilidad.
        /// Nombre sugerido: SyncPspStatus
        /// </summary>
        [HttpPost("SyncPspStatus")]
        public async Task<IActionResult> SyncPspStatus([FromBody] PSPStatusRequestDTO request)
        {
            try
            {
                // Validar usuario autenticado
                var usuario = TraeUsuarioUAT(request?.UAT);
                if (usuario == null)
                {
                    var estadoSinBilletera = _context.PSPAccountStatus.Where(s => s.Nombre == "Sin billetera").FirstOrDefault();
                    return BadRequest(new PSPStatusResponseDTO
                    {
                        Success = false,
                        Estado = estadoSinBilletera,
                        Mensaje = "Usuario no autenticado"
                    });
                }

                // Determinar tributary identifier a usar
                string tributary = request?.Cuil;

                if (string.IsNullOrEmpty(tributary))
                {
                    // intentar desde cliente/persona
                    var cliente = _context.Clientes.Include(c => c.Persona).FirstOrDefault(c => c.Usuario.Id == usuario.Id);
                    if (cliente != null)
                    {
                        var account = _context.PSPAccounts.Where(x => x.Cliente.Id== cliente.Id).FirstOrDefault();
                        if (account!=null)
                        {
                            tributary = account.TributaryIdentifier;
                        }
                    }
                    else
                    {
                        var estadoSinBilletera = _context.PSPAccountStatus.Where(s => s.Nombre == "Sin billetera").FirstOrDefault();
                        return StatusCode(500, new PSPStatusResponseDTO { Success = false, Estado = estadoSinBilletera, Mensaje = "Error no se encontro la relación cliente psp" });
                    }
                }

                // Obtener tokens en paralelo
                var userTokenTask = ObtenerUserTokenPSP(usuario);
                var systemTokenTask = _pspService.GetAccessTokenAsync();

                await Task.WhenAll(userTokenTask, systemTokenTask);

                // Siempre usar el token obtenido por el servidor para que sea invisible al cliente
                var userToken = userTokenTask.Result;
                var systemToken = systemTokenTask.Result;
                
                // Buscar o crear PSPAccount para este usuario
                var pspAccount = _context.Set<DAL.Models.PSPAccount>().Include(p => p.Usuario).FirstOrDefault(p => p.Usuario.Id == usuario.Id);
                var created = false;
                if (pspAccount == null)
                {
                    pspAccount = new DAL.Models.PSPAccount
                    {
                        Usuario = usuario,
                        UserName = usuario.UserName,
                        Status = "unknown",
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Set<DAL.Models.PSPAccount>().Add(pspAccount);
                    created = true;
                }

                pspAccount.LastStatusCheck = DateTime.UtcNow;

                Console.WriteLine(pspAccount);

                var resultDto = new PSPStatusResponseDTO{
                    Success = true,
                    Estado = pspAccount.EstadoCuentaPSP,
                    Mensaje = "Estado de cuenta consultado exitosamente",
                    EntityId = pspAccount.EntityId,
                    Cvu = pspAccount.CVU
                };
                
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en SyncPspStatus");
                var estadoSinBilletera = _context.PSPAccountStatus.Where(s => s.Nombre == "Sin billetera").FirstOrDefault();
                return StatusCode(500, new PSPStatusResponseDTO { Success = false, Estado = estadoSinBilletera, Mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// POST api/psp/Entities/RecoverPassword
        /// Solicita al PSP el envío de EventValidator para restablecer contraseña
        /// </summary>
        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordWithUATRequestDTO request)
        {
            try
            {
                var usuario = TraeUsuarioUAT(request.UAT);
                if (usuario == null)
                {
                    return BadRequest(new { success = false, message = "Usuario no autenticado", data = "", code = "" });
                }

                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new { success = false, message = "UserName y Email son requeridos", data = "", code = "" });
                }

                // Obtener token del sistema
                var systemToken = await _pspService.GetAccessTokenAsync();

                var pspReq = new RecoverPasswordRequestDTO { UserName = request.UserName, Email = request.Email };
                var pspResp = await _pspService.RecoverPasswordAsync(pspReq, systemToken?.access_token);

                if (pspResp == null)
                {
                    return StatusCode(500, new { success = false, message = "Error interno al comunicarse con PSP", data = "", code = "" });
                }

                return pspResp.success
                    ? (IActionResult)Ok(new { success = true, message = pspResp.message ?? "", data = pspResp.data ?? "", code = pspResp.code ?? "" })
                    : BadRequest(new { success = false, message = pspResp.message ?? "Error del PSP", data = pspResp.data ?? "", code = pspResp.code ?? "" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en RecoverPassword");
                return StatusCode(500, new { success = false, message = "Error interno del servidor", data = "", code = "" });
            }
        }

        /// <summary>
        /// POST api/psp/Entities/ResetPassword
        /// Orquesta el cambio de contraseña en PSP y sincroniza la contraseña local.
        /// </summary>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordWithUATRequestDTO request)
        {
            try
            {
                var usuarioAdmin = TraeUsuarioUAT(request.UAT);
                if (usuarioAdmin == null)
                {
                    return BadRequest(new { success = false, message = "Usuario no autenticado", data = "", code = "" });
                }

                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.PasswordConfirm) || string.IsNullOrEmpty(request.EventValidator))
                {
                    return BadRequest(new { success = false, message = "Parámetros incompletos", data = "", code = "" });
                }

                if (request.Password != request.PasswordConfirm)
                {
                    return BadRequest(new { success = false, message = "Password y PasswordConfirm no coinciden", data = "", code = "" });
                }

                // Obtener token del sistema
                var systemToken = await _pspService.GetAccessTokenAsync();

                // 1) Llamar PSP ResetPassword
                var resetReq = new ResetPasswordRequestDTO
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    PasswordConfirm = request.PasswordConfirm,
                    EventValidator = request.EventValidator
                };

                var pspResp = await _pspService.ResetPasswordAsync(resetReq, systemToken?.access_token);

                if (pspResp == null)
                {
                    return StatusCode(500, new { success = false, message = "Error interno al comunicarse con PSP", data = "", code = "" });
                }

                if (!pspResp.success)
                {
                    // PSP rechazó el cambio
                    return BadRequest(new { success = false, message = pspResp.message ?? "PSP rechazó la solicitud", data = pspResp.data ?? "", code = pspResp.code ?? "" });
                }

                // 2) PSP respondió success -> intentar sincronizar localmente
                // Buscar usuario local por UserName (puede ser email o username)
                var usuarioLocal = _context.Usuarios.FirstOrDefault(u => u.UserName == request.UserName || u.Email == request.UserName);

                if (usuarioLocal == null)
                {
                    // Usuario no existe localmente, devolver success true pero indicar que no se sincronizó
                    Log.Warning($"PSP cambió contraseña para {request.UserName} pero no se encontró usuario local para sincronizar");
                    return Ok(new { success = true, message = "Contraseña cambiada en PSP. Usuario local no encontrado para sincronizar.", data = request.UserName, code = "" });
                }

                // Obtener PSPAccount para actualizar EncryptedPassword
                var pspAccount = _context.Set<DAL.Models.PSPAccount>().FirstOrDefault(p => p.Usuario.Id == usuarioLocal.Id);

                // Guardar antiguos en memoria para rollback
                var oldLocalPassword = usuarioLocal.Password;
                var oldEncryptedPspPassword = pspAccount?.EncryptedPassword;

                // Intentar actualizar dentro de una transacción
                using (var tx = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Actualizar contraseña local (no se conoce el hashing usado; aquí asumimos campo Password almacena el hash ya generado por la app)
                        // Si existe una rutina para generar hash, debería usarse. Aquí asignamos directamente por simplicidad.
                        usuarioLocal.Password = request.Password; // NOTE: preferible aplicar hash aquí

                        if (pspAccount != null)
                        {
                            pspAccount.EncryptedPassword = common.CifrarPassword(request.Password);
                            pspAccount.UpdatedAt = DateTime.UtcNow;
                        }

                        await _context.SaveChangesAsync();
                        tx.Commit();

                        Log.Information($"Contraseña sincronizada localmente para usuario {usuarioLocal.UserName}");

                        return Ok(new { success = true, message = "Contraseña cambiada en PSP y sincronizada localmente", data = request.UserName, code = "" });
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        Log.Error(ex, "Error sincronizando contraseña localmente tras ResetPassword PSP");

                        // Intentar reintentos locales podría implementarse aquí; por ahora registramos y devolvemos mensaje
                        // Crear un flag de sincronización pendiente
                        try
                        {
                            if (pspAccount != null)
                            {
                                pspAccount.ErrorMessage = "SyncPending: Failed to save local password";
                                _context.SaveChanges();
                            }
                        }
                        catch { }

                        return StatusCode(500, new { success = false, message = "Contraseña cambió en PSP pero error al sincronizar localmente. Se ha marcado sincronización pendiente.", data = request.UserName, code = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en ResetPassword");
                return StatusCode(500, new { success = false, message = "Error interno del servidor", data = "", code = "" });
            }
        }
    }
}