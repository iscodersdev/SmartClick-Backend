using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.PSP
{
    public class RespuestaAPI
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }

    }

    #region ValidarCuentaExternaCBU

    public class ValidarCuantaExterna : RespuestaAPI
    {
        public string CBU { get; set; }
    }

    public class ApiResponsePSP
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ExternalAccountData Data { get; set; }
        public string Code { get; set; }
    }
    public class ExternalAccountData
    {
        public int ExternalAccountId { get; set; }
        public string AccountNumber { get; set; }
        public string DisplayName { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountTypeDescription { get; set; }
        public int CurrencyTypeId { get; set; }
        public string CurrencyTypeDescription { get; set; }
        public string CurrencyTypeName { get; set; }
        public string Label { get; set; }
        public string TributaryIdentifier { get; set; }
        public string TributaryIdentifierType { get; set; }
        public string PspBankDescription { get; set; }
        public bool Virtual { get; set; }
    }

    public class ExternalAccountDataDTO : RespuestaAPI
    {
        public int ExternoId { get; set; }
        public string NumeroDeCuenta { get; set; }
        public string Nombre { get; set; }
        public int TipoCuentaId { get; set; }
        public string DescripcionTipoCuenta { get; set; }
        public int TipoMonedaId { get; set; }
        public string DescripcionTipoDeMoneda { get; set; }
        public string NombreTipoDeMoneda { get; set; }
        public string Descipcion { get; set; }
        public string CUIT { get; set; }
        public string IdentificadorTributario { get; set; }
        public string BancoDescripcion { get; set; }
        public bool Virtual { get; set; }
        public bool Success { get; set; }
    }

    #endregion

    #region SolicitudDeTransferencia
    public class AccountRefDTO
    {
        public string accountNumber { get; set; }
        public int accountTypeId { get; set; }
        public string tributaryIdentifierType { get; set; }
        public string tributaryIdentifier { get; set; }
        public int currencyTypeId { get; set; }
        public string name { get; set; }
        public bool isExternal { get; set; }
    }

    public class TransactionRequestDTO
    {
        public string currencyTypeId { get; set; } = "1";
        public decimal balance { get; set; }
        public int transactionTypeId { get; set; }
        public string availabilityDate { get; set; }
        public string concept { get; set; }
        public string validationCode { get; set; }
        public bool isExternal { get; set; }
        public AccountRefDTO originAccount { get; set; }
        public AccountRefDTO destinationAccount { get; set; }
    }

    /*-------------------- Solicitud-------------------------*/

    public class GuidResultDTO
    {
        public string Key { get; set; }
        public string Message { get; set; }
    }

    public class TransactionResultDataDTO
    {
        public int TransactionId { get; set; }
        public string MessageResultTransfer { get; set; }
        public string TransactionInfoMessage { get; set; }
    }
    public class TransactionResponseDTO
    {
        public GuidResultDTO Guid { get; set; }
        public bool ViewModalOTP { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public TransactionResultDataDTO Data { get; set; }
        public string Code { get; set; }
    }

    public class TransactionResponse : RespuestaAPI
    {
        public string Key { get; set; }
        public bool ViewModalOTP { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public TransactionResultDataDTO Data { get; set; }
        public string Code { get; set; }
    }


    /*-------------------- Confirmar Transferencia-------------------------*/

    public class ConfirmationGuidDTO
    {
        public string Key { get; set; }
        public int Code { get; set; }
    }

    public class TransactionConfirmationRequestDTO
    {
        public int TransactionId { get; set; }
        public int OTP { get; set; }
        public bool IsExternal { get; set; }
        public ConfirmationGuidDTO Guid { get; set; }
    }

    public class TransactionFinalDataDTO
    {
        public int TransactionId { get; set; }
        public string MessageResultTransfer { get; set; }
        public bool ShowDownloadReceipt { get; set; }
    }

    public class FinalConfirmationResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TransactionFinalDataDTO Data { get; set; }
        public string Code { get; set; }
    }

    #endregion


}
