using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DAL.DTOs.PSP;
using System.Collections.Generic;

namespace BusinessCore.Services
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

        public PSPService(HttpClient httpClient, IConfiguration configuration, ILogger<PSPService> logger)
        {
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
                _logger.LogInformation("?? MODO PRUEBA: Simulando obtención de token");
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

                // SOLO agregar ClientId/ClientSecret si están configurados
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
                
                // SOLO agregar header X-client_id si tenemos ClientId válido
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
                _logger.LogError(ex, "Excepción al obtener token del PSP");
                return new TokenResponseDTO();
            }
        }

        public async Task<CreateEntityUserResponseDTO> CreateEntityAndUserAsync(CreateEntityUserRequestDTO request)
        {
            if (_testMode)
            {
                _logger.LogInformation("?? MODO PRUEBA: Simulando creación de entidad");
                _logger.LogInformation($"?? Datos simulados - CUIT: {request.entity.tributaryIdentifier}, Nombre: {request.entity.name}");
                
                await Task.Delay(500);
                
                return new CreateEntityUserResponseDTO 
                { 
                    Success = true, 
                    Message = "?? SIMULACIÓN: Entidad y usuario creados exitosamente (NO se guardó en PSP real)",
                    EntityId = 99999,
                    PersonId = 88888
                };
            }

            try
            {
                var tokenResponse = await GetAccessTokenAsync();
                if (string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    return new CreateEntityUserResponseDTO 
                    { 
                        Success = false, 
                        Error = "No se pudo obtener token de acceso" 
                    };
                }

                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {tokenResponse.access_token}");
                
                // SOLO agregar X-client_id si tenemos ClientId válido
                if (!string.IsNullOrEmpty(_clientId) && !_clientId.Contains("TU_CLIENT_ID"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);
                }

                _logger.LogWarning("?? EJECUTANDO CONTRA PSP REAL - Esto creará datos reales!");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}/multicuenta/api/v1/Entities/Persons/New", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Entidad creada exitosamente en PSP: {responseContent}");
                    
                    return new CreateEntityUserResponseDTO 
                    { 
                        Success = true, 
                        Message = "Entidad y usuario creados exitosamente" 
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error creando entidad en PSP: {response.StatusCode} - {errorContent}");
                    return new CreateEntityUserResponseDTO 
                    { 
                        Success = false, 
                        Error = $"Error del PSP: {response.StatusCode}" 
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al crear entidad en PSP");
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
                    ? "?? SIMULACIÓN: Entidad registrada (modo prueba)"
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

        public bool ValidateConfiguration()
        {
            if (_testMode)
            {
                return true; // En modo test no necesitamos credenciales reales
            }

            // NUEVA VALIDACIÓN: Solo requiere BaseUrl, Username y Password
            return !string.IsNullOrEmpty(_baseUrl) &&
                   !string.IsNullOrEmpty(_username) &&
                   !string.IsNullOrEmpty(_password);
            // ClientId y ClientSecret ahora son OPCIONALES
        }
    }
}