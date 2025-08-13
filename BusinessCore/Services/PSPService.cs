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
        }

        public async Task<TokenResponseDTO> GetAccessTokenAsync()
        {
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

                // Convertir a form-encoded como lo requiere la API
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", tokenRequest.grant_type),
                    new KeyValuePair<string, string>("username", tokenRequest.username),
                    new KeyValuePair<string, string>("password", tokenRequest.password),
                    new KeyValuePair<string, string>("client_secret", tokenRequest.client_secret),
                    new KeyValuePair<string, string>("client_id", tokenRequest.client_id)
                });

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);

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
            try
            {
                // Primero obtener el token
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
                _httpClient.DefaultRequestHeaders.Add("X-client_id", _clientId);

                var response = await _httpClient.PostAsync($"{_baseUrl}/multicuenta/api/v1/Entities/Persons/New", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Entidad creada exitosamente en PSP: {responseContent}");
                    
                    // Aquí puedes deserializar la respuesta específica del PSP si es necesario
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

                return new RegistrarEntidadResponseDTO
                {
                    Status = pspResponse.Success ? 200 : 500,
                    UAT = request.UAT,
                    Mensaje = pspResponse.Success ? "Entidad registrada exitosamente" : "Error al registrar entidad",
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
            return !string.IsNullOrEmpty(_baseUrl) &&
                   !string.IsNullOrEmpty(_clientId) &&
                   !string.IsNullOrEmpty(_clientSecret) &&
                   !string.IsNullOrEmpty(_username) &&
                   !string.IsNullOrEmpty(_password);
        }
    }
}