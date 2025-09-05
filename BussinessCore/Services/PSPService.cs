using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DAL.DTOs.PSP;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DAL.Data;
using DAL.Models;

// ? CAMBIAR ESTA L�NEA:
namespace BusinessCore.Services  // ? Cambiar de "SmartClickCore.Services" a "BusinessCore.Services"
{
    public class PSPService : IPSPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PSPService> _logger;
        private readonly string _baseUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _testMode;
        private readonly SmartClickContext _dbContext;

        public PSPService(HttpClient httpClient, IConfiguration configuration, ILogger<PSPService> logger, SmartClickContext context)
        {
            _dbContext = context;
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _baseUrl = _configuration["PSP:BaseUrl"];
            _clientId = _configuration["PSP:ClientId"];
            _clientSecret = _configuration["PSP:ClientSecret"];
            _username = _configuration["PSP:Username"];
            _password = _configuration["PSP:Password"];
            _testMode = _configuration.GetValue<bool>("PSP:TestMode", true); // Por defecto en modo test
        }

        public bool IsTestMode() => _testMode;

        public async Task<TokenResponseDTO> GetAccessTokenAsync()
        {
            if (_testMode)
            {
                _logger.LogInformation("?? MODO PRUEBA: Simulando obtenci�n de token");
                return new TokenResponseDTO
                {
                    access_token = "mock_token_12345",
                    token_type = "Bearer",
                    expires_in = 3600,
                    scope = "api"
                };
            }

            try
            {
                if (!ValidateConfiguration())
                {
                    _logger.LogError("PSP configuration is invalid");
                    return new TokenResponseDTO();
                }

                var tokenRequest = new TokenRequestDTO
                {
                    username = _username,
                    password = _password,
                    client_secret = _clientSecret,
                    client_id = _clientId
                };

                // Convertir a form-encoded - SOLO incluir campos que tenemos
                var formParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", tokenRequest.grant_type),
                    new KeyValuePair<string, string>("username", tokenRequest.username),
                    new KeyValuePair<string, string>("password", tokenRequest.password)
                };

                // SOLO agregar ClientId/ClientSecret si est�n configurados
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    formParams.Add(new KeyValuePair<string, string>("client_id", _clientId));
                }

                if (!string.IsNullOrEmpty(_clientSecret) && !_clientSecret.Contains("TU_CLIENT_SECRET"))
                {
                    formParams.Add(new KeyValuePair<string, string>("client_secret", _clientSecret));
                }

                var formContent = new FormUrlEncodedContent(formParams);

                _httpClient.DefaultRequestHeaders.Clear();
                
                // SOLO agregar header X-client_id si tenemos ClientId v�lido
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                }

                var response = await _httpClient.PostAsync($"{_baseUrl}/api/api/Account/Token", formContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponseDTO>(responseContent);
                    
                    _logger.LogInformation("Token obtenido exitosamente del PSP");
                    return tokenResponse;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error obteniendo token del PSP: {response.StatusCode} - {errorContent}");
                    return new TokenResponseDTO();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepci�n al obtener token del PSP");
                return new TokenResponseDTO();
            }
        }

        public async Task<TokenResponseDTO> GetAccessTokenUserAsync(string username, string password)
        {
            if (_testMode)
            {
                _logger.LogInformation("?? MODO PRUEBA: Simulando obtenci�n de token");
                return new TokenResponseDTO
                {
                    access_token = "mock_token_12345",
                    token_type = "Bearer",
                    expires_in = 3600,
                    scope = "api"
                };
            }

            try
            {
                if (!ValidateConfiguration())
                {
                    _logger.LogError("PSP configuration is invalid");
                    return new TokenResponseDTO();
                }

                var tokenRequest = new TokenRequestDTO
                {
                    username = username,
                    password = password,
                    client_secret = _clientSecret,
                    client_id = _clientId
                };

                // Convertir a form-encoded - SOLO incluir campos que tenemos
                var formParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", tokenRequest.grant_type),
                    new KeyValuePair<string, string>("username", tokenRequest.username),
                    new KeyValuePair<string, string>("password", tokenRequest.password)
                };

                // SOLO agregar ClientId/ClientSecret si est�n configurados
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    formParams.Add(new KeyValuePair<string, string>("client_id", _clientId));
                }

                if (!string.IsNullOrEmpty(_clientSecret) && !_clientSecret.Contains("TU_CLIENT_SECRET"))
                {
                    formParams.Add(new KeyValuePair<string, string>("client_secret", _clientSecret));
                }

                var formContent = new FormUrlEncodedContent(formParams);

                _httpClient.DefaultRequestHeaders.Clear();
                
                // SOLO agregar header X-client_id si tenemos ClientId v�lido
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                }

                var response = await _httpClient.PostAsync($"{_baseUrl}/api/api/Account/Token", formContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponseDTO>(responseContent);
                    
                    _logger.LogInformation("Token obtenido exitosamente del PSP");
                    return tokenResponse;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error obteniendo token del PSP: {response.StatusCode} - {errorContent}");
                    return new TokenResponseDTO();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepci�n al obtener token del PSP");
                return new TokenResponseDTO();
            }
        }

        public async Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO request)
        {
            if (_testMode)
            {
                _logger.LogInformation("?? MODO PRUEBA: Simulando creaci�n de usuario");
                return new CreateUserResponseDTO 
                { 
                    Success = true, 
                    Message = "?? SIMULACI�N: Usuario creado exitosamente (modo prueba)",
                    UserId = 77777,
                    UserToken = "mock_user_token_98765"
                };
            }

            try
            {
                var tokenResponse = await GetAccessTokenAsync();
                if (string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    _logger.LogError("No se pudo obtener token para crear usuario");
                    return new CreateUserResponseDTO 
                    { 
                        Success = false, 
                        Error = "No se pudo obtener token de acceso" 
                    };
                }

                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.access_token}");

                var response = await _httpClient.PostAsync($"{_baseUrl}/api/api/Account", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"PSP Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    var pspResponse = JsonConvert.DeserializeObject<CreateUserResponseDTO>(responseContent);
                    _logger.LogInformation($"Usuario creado exitosamente: UserId={pspResponse.UserId}, UserToken={pspResponse.UserToken}");
                    return pspResponse;
                }
                else
                {
                    _logger.LogError($"Error creando usuario en PSP: {response.StatusCode} - {responseContent}");
                    return new CreateUserResponseDTO 
                    { 
                        Success = false, 
                        Error = $"Error del PSP: {response.StatusCode}",
                        Message = responseContent
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepci�n al crear usuario en PSP");
                return new CreateUserResponseDTO 
                { 
                    Success = false, 
                    Error = ex.Message,
                    Message = "Error interno del sistema"
                };
            }
        }
        
        /// <summary>
        /// Crea una entidad asociada al usuario autenticado (SelfRegistration)
        /// </summary>
        /// 
        public async Task<SelfRegistrationResponseDTO> SelfRegistrationAsync(SelfRegistrationRequestDTO request, string userToken)
        {
            // PASO 1: Manejo del modo de prueba
            if (_testMode)
            {
                _logger.LogInformation("?? MODO PRUEBA: Simulando SelfRegistration");
                _logger.LogInformation($"?? Entidad simulada - CUIT: {request.tributaryIdentifier}, Nombre: {request.name}");
                
                await Task.Delay(400); // Simular latencia de red
                
                return new SelfRegistrationResponseDTO 
                { 
                    Success = true, 
                    Message = "?? SIMULACI�N: Entidad creada exitosamente (modo prueba)",
                    Identifier = "mock-identifier-12345-abcdef",
                    EntityId = 66666
                };
            }

            try
            {
                // PASO 2: Validar que tenemos un token de usuario
                if (string.IsNullOrEmpty(userToken))
                {
                    _logger.LogError("No se proporcion� token de usuario para SelfRegistration");
                    return new SelfRegistrationResponseDTO 
                    { 
                        Success = false, 
                        Error = "Token de usuario requerido" 
                    };
                }

                // PASO 3: Preparar el request JSON
                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // PASO 4: Configurar headers HTTP con el token del USUARIO (no del sistema)
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {userToken}");
                
                // Header adicional si tenemos ClientId v�lido
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                }

                _logger.LogInformation($"Ejecutando SelfRegistration en PSP - CUIT: {request.tributaryIdentifier}");

                // PASO 5: Realizar la llamada HTTP
                var response = await _httpClient.PostAsync($"{_baseUrl}/multicuenta/api/v1/Accounts/SelfRegistration", content);

                // PASO 6: Procesar la respuesta
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"SelfRegistration completado exitosamente en PSP: {responseContent}");
                    
                    // TODO: Deserializar la respuesta real para obtener el Identifier
                    // Por ahora devolvemos una respuesta gen�rica exitosa
                    return new SelfRegistrationResponseDTO 
                    { 
                        Success = true, 
                        Message = "Entidad creada exitosamente mediante SelfRegistration",
                        // Identifier y EntityId se deber�an extraer de responseContent
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error en SelfRegistration del PSP: {response.StatusCode} - {errorContent}");
                    
                    return new SelfRegistrationResponseDTO 
                    { 
                        Success = false, 
                        Error = $"Error del PSP: {response.StatusCode}",
                        Message = errorContent
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Excepci�n en SelfRegistration - CUIT: {request.tributaryIdentifier}");
                return new SelfRegistrationResponseDTO 
                { 
                    Success = false, 
                    Error = ex.Message,
                    Message = "Error interno del sistema"
                };
            }
        }

        public async Task<CreateEntityUserResponseDTO> CreateEntityAndUserAsync(CreateEntityUserRequestDTO request)
        {
            try
            {
                // Obtener token del sistema primero
                var tokenResponse = await GetAccessTokenAsync();
                if (string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    return new CreateEntityUserResponseDTO 
                    { 
                        Success = false, 
                        Error = "No se pudo obtener token de acceso" 
                    };
                }

                // Preparar el JSON request
                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Configurar headers HTTP
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {tokenResponse.access_token}");
                
                // Agregar X-client_id si est� configurado
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                }

                _logger.LogInformation($"Creando entidad y usuario en PSP REAL - CUIT: {request.entity.tributaryIdentifier}, UserName: {request.person.userName}");

                // Realizar la llamada HTTP al PSP
                var response = await _httpClient.PostAsync($"{_baseUrl}/multicuenta/api/v1/Entities/Persons/New", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Entidad y usuario creados exitosamente en PSP: {responseContent}");
            
                    // TODO: Deserializar la respuesta real para obtener EntityId y PersonId
                    // Por ahora devolvemos una respuesta exitosa gen�rica
                    return new CreateEntityUserResponseDTO 
                    { 
                        Success = true, 
                        Message = "Entidad y usuario creados exitosamente"
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error creando entidad y usuario en PSP: {response.StatusCode} - {errorContent}");
            
                    return new CreateEntityUserResponseDTO 
                    { 
                        Success = false, 
                        Error = $"Error del PSP: {response.StatusCode}",
                        Message = errorContent
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Excepci�n al crear entidad y usuario - CUIT: {request.entity?.tributaryIdentifier}");
                return new CreateEntityUserResponseDTO 
                { 
                    Success = false, 
                    Error = ex.Message 
                };
            }
        }

        public async Task<RegistrarEntidadResponseDTO> RegistrarEntidadAsync(RegistrarEntidadRequestDTO request)
        {
            try
            {
                // Convertir el request simplificado al formato completo del PSP
                var pspRequest = new CreateEntityUserRequestDTO
                {
                    entity = new EntityDTO
                    {
                        tributaryIdentifier = request.TributaryIdentifier,
                        name = request.Name,
                        email = request.Email,
                        phone = request.Phone,
                        address = request.Address,
                        cityId = request.CityId,
                        postalCode = request.PostalCode,
                        activityAddress = request.Address,
                        activityCityId = request.CityId,
                        activityPostalCode = request.PostalCode
                    },
                    person = new PersonDTO
                    {
                        documentNumber = request.DocumentNumber,
                        name = request.FirstName,
                        lastName = request.LastName,
                        userName = request.UserName,
                        phone = request.Phone,
                        address = request.Address,
                        cityId = request.CityId,
                        province = request.Province,
                        email = request.Email
                    }
                };

                var pspResponse = await CreateEntityAndUserAsync(pspRequest);

                var mensaje = _testMode 
                    ? "?? SIMULACI�N: Entidad registrada (modo prueba)"
                    : "Entidad registrada exitosamente";

                return new RegistrarEntidadResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = request.UAT,
                    Mensaje = pspResponse.Success ? mensaje : "Error al registrar entidad",
                    Success = pspResponse.Success,
                    EntityId = pspResponse.EntityId,
                    PersonId = pspResponse.PersonId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en RegistrarEntidadAsync");
                return new RegistrarEntidadResponseDTO
                {
                    Status = 500,
                    UAT = request.UAT,
                    Mensaje = "Error interno del servidor",
                    Success = false
                };
            }
        }

        // Agregar este m�todo para el nuevo requerimiento de subir archivos

        /// <summary>
        /// Sube archivos de validaci�n para una entidad (DNI, selfie, etc.)
        /// </summary>
        public async Task<UploadFilesResponseDTO> UploadFilesAsync(string identifier, string userToken, Dictionary<string, byte[]> files)
        {
            // PASO 1: Manejo del modo de prueba
            if (_testMode)
            {
                _logger.LogInformation("?? MODO PRUEBA: Simulando subida de archivos");
                _logger.LogInformation($"?? Archivos simulados - Identifier: {identifier}, Cantidad: {files.Count}");
                
                await Task.Delay(600); // Simular latencia de upload
                
                var uploadedFiles = files.Keys.ToList();
                
                return new UploadFilesResponseDTO 
                { 
                    Success = true, 
                    Message = "?? SIMULACI�N: Archivos subidos exitosamente (modo prueba)",
                    UploadedFiles = uploadedFiles
                };
            }

            try
            {
                // PASO 2: Validar par�metros requeridos
                if (string.IsNullOrEmpty(identifier))
                {
                    _logger.LogError("No se proporcion� Identifier para subir archivos");
                    return new UploadFilesResponseDTO 
                    { 
                        Success = false, 
                        Error = "Identifier requerido" 
                    };
                }

                if (string.IsNullOrEmpty(userToken))
                {
                    _logger.LogError("No se proporcion� token de usuario para subir archivos");
                    return new UploadFilesResponseDTO 
                    { 
                        Success = false, 
                        Error = "Token de usuario requerido" 
                    };
                }

                if (files == null || !files.Any())
                {
                    _logger.LogError("No se proporcionaron archivos para subir");
                    return new UploadFilesResponseDTO 
                    { 
                        Success = false, 
                        Error = "Al menos un archivo es requerido" 
                    };
                }

                // PASO 3: Preparar MultipartFormDataContent
                using (var formData = new MultipartFormDataContent())
                {
                    // Agregar cada archivo al form-data
                    foreach (var file in files)
                    {
                        var fileContent = new ByteArrayContent(file.Value);
                        fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                        formData.Add(fileContent, file.Key, $"{file.Key}.jpg"); // Nombre de archivo gen�rico
                    }

                    // PASO 4: Configurar headers HTTP con el token del USUARIO
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {userToken}");
                    
                    // Header adicional si tenemos ClientId v�lido
                    if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                    {
                        _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                    }

                    _logger.LogInformation($"Subiendo archivos al PSP - Identifier: {identifier}, Archivos: {string.Join(", ", files.Keys)}");

                    // PASO 5: Realizar la llamada HTTP
                    var response = await _httpClient.PostAsync($"{_baseUrl}/multicuenta/api/v1/Accounts/SelfRegistration/Files?Identifier={identifier}", formData);

                    // PASO 6: Procesar la respuesta
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"Archivos subidos exitosamente al PSP - Identifier: {identifier}");
                        
                        return new UploadFilesResponseDTO 
                        { 
                            Success = true, 
                            Message = "Archivos subidos exitosamente",
                            UploadedFiles = files.Keys.ToList()
                        };
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        _logger.LogError($"Error subiendo archivos al PSP: {response.StatusCode} - {errorContent}");
                        
                        return new UploadFilesResponseDTO 
                        { 
                            Success = false, 
                            Error = $"Error del PSP: {response.StatusCode}",
                            Message = errorContent
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Excepci�n al subir archivos - Identifier: {identifier}");
                return new UploadFilesResponseDTO 
                { 
                    Success = false, 
                    Error = ex.Message,
                    Message = "Error interno del sistema"
                };
            }
        }

        // Agregar estos m�todos despu�s de UploadFilesAsync

        /// <summary>
        /// Obtiene la lista de provincias disponibles
        /// </summary>
        public async Task<ProvincesResponseDTO> GetProvincesAsync()
        {
            try
            {
                // No se requiere autenticaci�n ni headers especiales
                _httpClient.DefaultRequestHeaders.Clear();

                // Llama al endpoint real del PSP
                var response = await _httpClient.GetAsync($"{_baseUrl}/multicuenta/api/v1/Province");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Provincias obtenidas exitosamente del PSP");

                    // Deserializa la respuesta real del PSP
                    var apiResponse = JsonConvert.DeserializeObject<ApiProvincesResponse>(responseContent);

                    return new ProvincesResponseDTO
                    {
                        Success = apiResponse.success,
                        Message = apiResponse.message,
                        Provinces = apiResponse.data
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error obteniendo provincias del PSP: {response.StatusCode} - {errorContent}");

                    return new ProvincesResponseDTO
                    {
                        Success = false,
                        Error = $"Error del PSP: {response.StatusCode}",
                        Message = errorContent
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepci�n al obtener provincias del PSP");
                return new ProvincesResponseDTO
                {
                    Success = false,
                    Error = ex.Message,
                    Message = "Error interno del sistema"
                };
            }
        }

        /// <summary>
        /// Obtiene la lista de ciudades de una provincia espec�fica
        /// </summary>
        public async Task<CitiesResponseDTO> GetCitiesAsync(int provinceId)
        {
            if (_testMode)
            {
                _logger.LogInformation($"?? MODO PRUEBA: Simulando obtenci�n de ciudades para provincia {provinceId}");
                
                await Task.Delay(250); // Simular latencia
                
                var mockCities = new List<CityDTO>
                {
                    new CityDTO { id = 1, name = "La Plata", provinceId = provinceId, postalCode = "1900" },
                    new CityDTO { id = 2, name = "Mar del Plata", provinceId = provinceId, postalCode = "7600" },
                    new CityDTO { id = 3, name = "C�rdoba Capital", provinceId = provinceId, postalCode = "5000" },
                    new CityDTO { id = 17934, name = "Ciudad Ejemplo", provinceId = provinceId, postalCode = "1234" }
                };
                
                return new CitiesResponseDTO 
                { 
                    Success = true, 
                    Message = $"?? SIMULACI�N: Ciudades obtenidas para provincia {provinceId} (modo prueba)",
                    Cities = mockCities
                };
            }

            try
            {
                _httpClient.DefaultRequestHeaders.Clear();

                // Obtener token antes de llamar
                var tokenResponse = await GetAccessTokenAsync();
                if (string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    return new CitiesResponseDTO
                    {
                        Success = false,
                        Error = "No se pudo obtener token de acceso",
                        Message = "No autenticado"
                    };
                }

                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.access_token}");

                // Si tu API requiere X-client_id, agr�galo tambi�n
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                }

                var response = await _httpClient.GetAsync($"{_baseUrl}/multicuenta/api/v1/City?provinceId={provinceId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Ciudades obtenidas exitosamente del PSP para provincia {provinceId}");

                    var apiResponse = JsonConvert.DeserializeObject<ApiCitiesResponse>(responseContent);

                    return new CitiesResponseDTO
                    {
                        Success = apiResponse.success,
                        Message = apiResponse.message,
                        Cities = apiResponse.data
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error obteniendo ciudades del PSP: {response.StatusCode} - {errorContent}");

                    return new CitiesResponseDTO
                    {
                        Success = false,
                        Error = $"Error del PSP: {response.StatusCode}",
                        Message = errorContent
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Excepci�n al obtener ciudades para provincia {provinceId}");
                return new CitiesResponseDTO 
                { 
                    Success = false, 
                    Error = ex.Message,
                    Message = "Error interno del sistema"
                };
            }
        }

        /// <summary>
        /// Obtiene la informaci�n de las cuentas del usuario logueado
        /// </summary>
        public async Task<AccountsInfoResponseDTO> GetAccountsInfoAsync(string userToken)
        {
            try
            {
                // Validar que tenemos un token de usuario
                if (string.IsNullOrEmpty(userToken))
                {
                    _logger.LogError("No se proporcion� token de usuario para obtener informaci�n de cuentas");
                    return new AccountsInfoResponseDTO
                    {
                        Success = false,
                        Error = "Token de usuario requerido"
                    };
                }

                // Configurar headers HTTP con el token del usuario
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {userToken}");
                
                // Header adicional si tenemos ClientId v�lido
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                }

                var requestUrl = $"{_baseUrl}/multicuenta/api/v1/Accounts/All/Get";
                _logger.LogInformation($"?? LLAMADA PSP - URL: {requestUrl}");
                _logger.LogInformation($"?? HEADERS - Authorization: Bearer {userToken.Substring(0, Math.Min(20, userToken.Length))}...");
                if (!string.IsNullOrEmpty(_clientId))
                {
                    _logger.LogInformation($"?? HEADERS - X-client_id: {_clientId}");
                }

                // Realizar la llamada HTTP al endpoint real del PSP
                var response = await _httpClient.GetAsync(requestUrl);

                var responseContent = await response.Content.ReadAsStringAsync();
                
                _logger.LogInformation($"?? PSP RESPONSE - StatusCode: {response.StatusCode}");
                _logger.LogInformation($"?? PSP RESPONSE - Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("? Informaci�n de cuentas obtenida exitosamente del PSP");

                    // Primero intentamos deserializar para ver la estructura real
                    try 
                    {
                        // Intentar deserializar como objeto din�mico primero
                        var dynamicResponse = JsonConvert.DeserializeObject(responseContent);
                        _logger.LogInformation($"?? RESPUESTA DESERIALIZADA: {JsonConvert.SerializeObject(dynamicResponse, Formatting.Indented)}");

                        // Ahora intentar con nuestra estructura esperada
                        var apiResponse = JsonConvert.DeserializeObject<ApiAccountsResponse>(responseContent);
                        _logger.LogInformation($"?? API RESPONSE - Success: {apiResponse?.success}");
                        _logger.LogInformation($"?? API RESPONSE - Message: {apiResponse?.message}");
                        _logger.LogInformation($"?? API RESPONSE - Accounts Count: {apiResponse?.accounts?.Count ?? 0}");

                        if (apiResponse?.accounts != null && apiResponse.accounts.Any())
                        {
                            foreach (var account in apiResponse.accounts)
                            {
                                _logger.LogInformation($"?? CUENTA - AccountNumber: {account.accountNumber}, EntityId: {account.entityId}, Name: {account.name}");
                            }
                        }

                        return new AccountsInfoResponseDTO
                        {
                            Success = apiResponse?.success ?? false,
                            Message = "Informaci�n de cuentas obtenida exitosamente",
                            Accounts = apiResponse?.accounts ?? new List<AccountInfoDTO>()
                        };
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError(jsonEx, $"? Error deserializando respuesta del PSP: {responseContent}");
                        
                        return new AccountsInfoResponseDTO
                        {
                            Success = false,
                            Error = "Error deserializando respuesta del PSP",
                            Message = $"Respuesta del PSP: {responseContent}"
                        };
                    }
                }
                else
                {
                    _logger.LogError($"? Error obteniendo informaci�n de cuentas del PSP: {response.StatusCode} - {responseContent}");

                    return new AccountsInfoResponseDTO
                    {
                        Success = false,
                        Error = $"Error del PSP: {response.StatusCode}",
                        Message = responseContent
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "? Excepci�n al obtener informaci�n de cuentas del PSP");
                return new AccountsInfoResponseDTO
                {
                    Success = false,
                    Error = ex.Message,
                    Message = "Error interno del sistema"
                };
            }
        }

        public bool ValidateConfiguration()
        {
            if (_testMode)
            {
                return true; // En modo test no necesitamos credenciales reales
            }

            // NUEVA VALIDACI�N: Solo requiere BaseUrl, Username y Password
            return !string.IsNullOrEmpty(_baseUrl) &&
                   !string.IsNullOrEmpty(_username) &&
                   !string.IsNullOrEmpty(_password);
            // ClientId y ClientSecret ahora son OPCIONALES
        }
    }

    public class ApiProvincesResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<ProvinceDTO> data { get; set; }
    }

    public class ApiCitiesResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<CityDTO> data { get; set; }
    }

    public class ApiAccountsResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<AccountInfoDTO> accounts { get; set; }
    }
}