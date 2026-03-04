using DAL.DTOs.PSP;
using DAL.Models;
using DAL.Models.PSP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessCore.Services
{
    public partial class PSPService
    {
        // TODO: Cambiar account number en produccion
        private string _nroCuentaRecaudadora = "30717072509-00000591";
        
        /// <summary>
        /// Valida una cuenta externa en el PSP
        /// </summary>
        /// <param name="CBU">CBU de la cuenta destino</param>
        /// <param name="userToken">Token PSP de la cuenta logueada</param>
        /// <returns></returns>
        public async Task<ExternalAccountDataDTO> ValidarCuentaExternaAsync(string CBU, string userToken)
        {
            try
            {
                CuentasRecaudadoras cuentaRecaudadora = _context.CuentasRecaudadoras.Where(x => x.AccountNumber==_nroCuentaRecaudadora).FirstOrDefault();
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{cuentaRecaudadora.BaseUrl}/a/multicuenta/api/v1/Person/ContactNotebook/Get"); 
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);
                requestMessage.Headers.Add("X-client_id", cuentaRecaudadora.ClientId);

                var requestBody = new { textSearch = CBU };
                var jsonContent = JsonConvert.SerializeObject(requestBody);
                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    JObject respuestaCompleta = JObject.Parse(content);
                    string mensajeExterno = respuestaCompleta["message"].ToString();
                    return new ExternalAccountDataDTO { Status = (int)response.StatusCode, Mensaje = mensajeExterno };
                }

                var apiResponse = JsonConvert.DeserializeObject<ApiResponsePSP>(content);
                return new ExternalAccountDataDTO
                {
                     ExternoId = apiResponse.Data.ExternalAccountId,
                     NumeroDeCuenta = apiResponse.Data.AccountNumber,
                     Nombre = apiResponse.Data.DisplayName,
                     TipoCuentaId = apiResponse.Data.AccountTypeId,
                     DescripcionTipoCuenta = apiResponse.Data.AccountTypeDescription,
                     TipoMonedaId = apiResponse.Data.CurrencyTypeId,
                     DescripcionTipoDeMoneda = apiResponse.Data.CurrencyTypeDescription,
                     NombreTipoDeMoneda = apiResponse.Data.CurrencyTypeName,
                     Descipcion = apiResponse.Data.Label,
                     CUIT = apiResponse.Data.TributaryIdentifier,
                     IdentificadorTributario = apiResponse.Data.TributaryIdentifierType,
                     BancoDescripcion = apiResponse.Data.PspBankDescription,
                     Virtual = apiResponse.Data.Virtual,
                     Mensaje = apiResponse.Message,
                     Success = apiResponse.Success 
                };
            }
            catch (Exception ex)
            {
                return new ExternalAccountDataDTO { Status = 500, Mensaje = ex.Message };
            }
        }


        /// <summary>
        /// Valida una cuenta externa en el PSP
        /// </summary>
        /// <param name="CBU">CBU de la cuenta destino</param>
        /// <param name="userToken">Token PSP de la cuenta logueada</param>
        /// <returns></returns>
        public async Task<AgendarCuentaDataDTO> AgendarCuentaExternaAsync(int externalId, string userToken)
        {
            try
            {
                CuentasRecaudadoras cuentaRecaudadora = _context.CuentasRecaudadoras.Where(x => x.AccountNumber==_nroCuentaRecaudadora).FirstOrDefault();
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{cuentaRecaudadora.BaseUrl}/a/multicuenta/api/v2/Person/ContactNotebook/Add"); 
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);
                requestMessage.Headers.Add("X-client_id", cuentaRecaudadora.ClientId);

                var requestBody = new { ExternalAccountId = externalId };
                var jsonContent = JsonConvert.SerializeObject(requestBody);
                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    JObject respuestaCompleta = JObject.Parse(content);
                    string mensajeExterno = respuestaCompleta["message"].ToString();
                    return new AgendarCuentaDataDTO { Status = (int)response.StatusCode, Mensaje = mensajeExterno, Success = false };
                }

                var apiResponse = JsonConvert.DeserializeObject<ApiResponsePSP>(content);
                return new AgendarCuentaDataDTO
                {
                     Mensaje = apiResponse.Message,
                     Status = (int)response.StatusCode,
                     Success = apiResponse.Success
                };
            }
            catch (Exception ex)
            {
                return new AgendarCuentaDataDTO { Status = 500, Mensaje = ex.Message };
            }
        }
        
        

       /// <summary>
       /// Solicita una tranferencia
       /// </summary>
       /// <param name="cuentaOrigen"></param>
       /// <param name="cuentaDestino"></param>
       /// <param name="transferenciaExterna"></param>
       /// <param name="monto"></param>
       /// <param name="userToken"></param>
       /// <returns></returns>
        public async Task<TransactionResponseDTO> SolicitudDeTransferenciaAsync(PSPAccount cuentaOrigen, ExternalAccountDataDTO cuentaDestino, bool transferenciaExterna, string monto, string userToken)
        {
            try
            {
                CuentasRecaudadoras cuentaRecaudadora = _context.CuentasRecaudadoras.Where(x => x.AccountNumber=="30717072509-00000591").FirstOrDefault();
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{cuentaRecaudadora.BaseUrl}/a/multicuenta/api/v1/Accounts/Transactions/Add");

                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);
                requestMessage.Headers.Add("X-client_id", cuentaRecaudadora.ClientId);

                TransactionRequestDTO transactionRequestDTO = new TransactionRequestDTO()
                {
                    balance = Convert.ToDecimal(monto),
                    transactionTypeId = 1,
                    concept = "VAR",
                    isExternal = transferenciaExterna,
                    originAccount = new AccountRefDTO()
                    {
                        accountNumber = cuentaOrigen.AccountNumber,
                        accountTypeId = 0
                    },
                    destinationAccount = new AccountRefDTO()
                    {
                        accountNumber = cuentaDestino.NumeroDeCuenta,
                        tributaryIdentifierType = cuentaDestino.IdentificadorTributario,
                        tributaryIdentifier = cuentaDestino.CUIT,
                    }
                };


                var jsonContent = JsonConvert.SerializeObject(transactionRequestDTO);
                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    JObject respuestaCompleta = JObject.Parse(content);
                    string mensajeExterno = respuestaCompleta["message"]?.ToString() ?? "Error desconocido al intentar la transferencia.";
                    return new TransactionResponseDTO { Code = response.StatusCode.ToString(), Message = mensajeExterno, Success = false };
                }

                var apiResponse = JsonConvert.DeserializeObject<TransactionResponseDTO>(content);

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new TransactionResponseDTO { Code = "500", Message = "Excepci�n al ejecutar la transferencia: " + ex.Message, Success = false };
            }
        }




        /// <summary>
        /// Copnfirma una tranferencia
        /// </summary>
        /// <param name="confirmarTransferencia"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public async Task<FinalConfirmationResponseDTO> ConfirmarTransferenciaAsync(TransactionConfirmationRequestDTO confirmarTransferencia, string userToken)
        {
            try
            {
                CuentasRecaudadoras cuentaRecaudadora = _context.CuentasRecaudadoras.Where(x => x.AccountNumber=="30717072509-00000591").FirstOrDefault();
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{cuentaRecaudadora.BaseUrl}/a/multicuenta/api/v1/Accounts/Transactions/Confirmation");

                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);
                requestMessage.Headers.Add("X-client_id", cuentaRecaudadora.ClientId);

                var jsonContent = JsonConvert.SerializeObject(confirmarTransferencia);
                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    JObject respuestaCompleta = JObject.Parse(content);
                    string mensajeExterno = respuestaCompleta["message"]?.ToString() ?? "Error desconocido al intentar la transferencia.";
                    return new FinalConfirmationResponseDTO { Code = response.StatusCode.ToString(), Message = mensajeExterno, Success = false };
                }

                var apiResponse = JsonConvert.DeserializeObject<FinalConfirmationResponseDTO>(content);

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new FinalConfirmationResponseDTO { Code = "500", Message = "Excepci�n al ejecutar la transferencia: " + ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Consultar Saldo
        /// </summary>
        /// <returns></returns>
        public async Task<BalanceResponseDTO> ConsultarSaldoAsync(string accountNumber, string userToken)
        {
            try
            {
                CuentasRecaudadoras cuentaRecaudadora = _context.CuentasRecaudadoras.Where(x => x.AccountNumber=="30717072509-00000591").FirstOrDefault();
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{cuentaRecaudadora.BaseUrl}/a/multicuenta/api/v1/Accounts/Balances");

                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);
                requestMessage.Headers.Add("X-client_id", cuentaRecaudadora.ClientId);

                ConsultarSaldoDTO consultarSaldoRequestDTO = new ConsultarSaldoDTO()
                {
                    accountNumber = accountNumber,
                };


                var jsonContent = JsonConvert.SerializeObject(consultarSaldoRequestDTO);
                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    JObject respuestaCompleta = JObject.Parse(content);
                    string mensajeExterno = respuestaCompleta["message"]?.ToString() ?? "Error desconocido al intentar la transferencia.";
                    return new BalanceResponseDTO { Code = response.StatusCode.ToString(), Message = mensajeExterno, Success = false };
                }

                var apiResponse = JsonConvert.DeserializeObject<BalanceResponseDTO>(content);

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new BalanceResponseDTO { Code = "500", Message = "Excepci�n al ejecutar la transferencia: " + ex.Message, Success = false };
            }
        }



        /// <summary>
        /// Copnfirma una tranferencia
        /// </summary>
        /// <param name="confirmarTransferencia"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public async Task<FinalConfirmationResponseDTO> TransferenciaCuentaRecaudadoraAsync(PSPAccount cuentaOrigen, string monto)
        {
            try
            {

                HttpResponseMessage response = new HttpResponseMessage();
                TransactionResponseDTO apiResponse = new TransactionResponseDTO();

                CuentasRecaudadoras cuentaRecaudadora = _context.CuentasRecaudadoras.Where(x => x.AccountNumber=="30717072509-00000591").FirstOrDefault();
                TokenResponseDTO token = await GetAccessTokenAsync();

                using (var client = new HttpClient())
                {
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{cuentaRecaudadora.BaseUrl}/a/multicuenta/api/v1/Accounts/Transactions/Add");

                    requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);
                    requestMessage.Headers.Add("X-client_id", cuentaRecaudadora.ClientId);

                    TransactionRequestDTO transactionRequestDTO = new TransactionRequestDTO()
                    {
                        balance = Convert.ToDecimal(monto),
                        transactionTypeId = 1,
                        concept = "VAR",
                        isExternal = false,
                        originAccount = new AccountRefDTO()
                        {
                            accountNumber = cuentaRecaudadora.AccountNumber,
                            accountTypeId = 1
                        },
                        destinationAccount = new AccountRefDTO()
                        {
                            accountNumber = cuentaOrigen.AccountNumber,
                            tributaryIdentifierType = cuentaOrigen.TributaryIdentifierType,
                            tributaryIdentifier = cuentaOrigen.TributaryIdentifier,
                            accountTypeId = 1
                        }
                    };

                    var jsonContent = JsonConvert.SerializeObject(transactionRequestDTO);
                    requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    response = await client.SendAsync(requestMessage);
                    var content = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<TransactionResponseDTO>(content);
                }

                if (response.IsSuccessStatusCode)
                {

                    var requestMessageConfirmacion = new HttpRequestMessage(HttpMethod.Post, $"{cuentaRecaudadora.BaseUrl}/a/multicuenta/api/v1/Accounts/Transactions/Confirmation");

                    requestMessageConfirmacion.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);
                    requestMessageConfirmacion.Headers.Add("X-client_id", cuentaRecaudadora.ClientId);

                    TransactionConfirmationRequestDTO confirmarTrans = new TransactionConfirmationRequestDTO()
                    {
                        Guid = new ConfirmationGuidDTO()
                        {
                            Key = apiResponse.Guid.Key,
                            Code = 999999
                        },
                        OTP = 999999,
                        TransactionId = apiResponse.Data.TransactionId,
                        IsExternal = false
                    };

                    var jsonContentConfirmarTrans = JsonConvert.SerializeObject(confirmarTrans);
                    requestMessageConfirmacion.Content = new StringContent(jsonContentConfirmarTrans, Encoding.UTF8, "application/json");
                    using (var clientConfirmacion = new HttpClient())
                    {

                        var responseConfirmacion = await clientConfirmacion.SendAsync(requestMessageConfirmacion);
                        var contentConfirmacion = await response.Content.ReadAsStringAsync();                     

                        if (!responseConfirmacion.IsSuccessStatusCode)
                        {
                            JObject respuestaCompleta = JObject.Parse(contentConfirmacion);
                            string mensajeExterno = respuestaCompleta["message"]?.ToString() ?? "Error desconocido al intentar la transferencia.";
                            return new FinalConfirmationResponseDTO { Code = response.StatusCode.ToString(), Message = mensajeExterno, Success = false };
                        }

                        var apiResponseConfirmacion = JsonConvert.DeserializeObject<FinalConfirmationResponseDTO>(contentConfirmacion);
                        return apiResponseConfirmacion;
                    }
                }
                
                return new FinalConfirmationResponseDTO { Code = "500", Message = "Excepci�n al ejecutar la transferencia: ", Success = false };

            }
            catch (Exception ex)
            {
                return new FinalConfirmationResponseDTO { Code = "500", Message = "Excepci�n al ejecutar la transferencia: " + ex.Message, Success = false };
            }
        }
    }
}