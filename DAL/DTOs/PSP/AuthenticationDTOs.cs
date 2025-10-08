using System;
using System.Collections.Generic;

namespace DAL.DTOs.PSP
{
    // DTOs para autenticación y token
    public class TokenRequestDTO
    {
        public string grant_type { get; set; } = "password";
        public string username { get; set; }
        public string password { get; set; }
        public string client_secret { get; set; }
        public string client_id { get; set; }
    }

    public class TokenResponseDTO
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }

    // DTOs para crear entidad y usuario
    public class CreateEntityUserRequestDTO
    {
        public EntityDTO entity { get; set; }
        public PersonDTO person { get; set; }
    }

    public class EntityDTO
    {
        public int entityTypeId { get; set; } = 5; // Persona Física
        public int parentId { get; set; } = 1601;
        public int? commercialPlanId { get; set; }
        public bool isPhysicalPerson { get; set; } = false;
        public bool? taxPayer { get; set; }
        public bool IsPyME { get; set; } = false;
        public DateTime? PyMEEffectiveDate { get; set; }
        public string tributaryIdentifierType { get; set; } = "CUIT";
        public string tributaryIdentifier { get; set; }
        public string name { get; set; }
        public string FantasyName { get; set; }
        public string CUF { get; set; }
        public string CovenantCode { get; set; }
        public int singleTransactionApprovalStages { get; set; } = 0;
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int cityId { get; set; }
        public string floor { get; set; }
        public string department { get; set; }
        public string postalCode { get; set; }
        public bool IsSameAddress { get; set; } = true;
        public string activityAddress { get; set; }
        public int? activityProvince { get; set; }
        public int activityCityId { get; set; }
        public string activityFloor { get; set; }
        public string activityDepartment { get; set; }
        public string activityPostalCode { get; set; }
        public string CreateUser { get; set; }
        public List<object> files { get; set; } = new List<object>();
        public string phoneCode { get; set; } = "549";
    }

    public class PersonDTO
    {
        public string documentType { get; set; } = "CUIL";
        public string documentNumber { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int cityId { get; set; }
        public int province { get; set; }
        public string email { get; set; }
        public string phoneCode { get; set; } = "549";
        public List<int> roles { get; set; } = new List<int> { 9 }; // TransactionViewer
    }

    public class CreateEntityUserResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string EntityId { get; set; }
        public int? PersonId { get; set; }
        public string Error { get; set; }
    }

    // DTOs base para respuestas de API
    public class PSPBaseResponseDTO
    {
        public int Status { get; set; }
        public string UAT { get; set; }
        public string Mensaje { get; set; }
        public bool Success { get; set; }
    }

    // DTO para Lookup de cuenta externa
    public class ExternalAccountLookupResponseDTO
    {
        public bool success { get; set; }
        public string message { get; set; }
        public ExternalAccountData data { get; set; }
        public string code { get; set; }
    }

    public class ExternalAccountData
    {
        public int externalAccountId { get; set; }
        public string accountNumber { get; set; }
        public string displayName { get; set; }
        public int accountTypeId { get; set; }
        public string accountTypeDescription { get; set; }
        public int currencyTypeId { get; set; }
        public string currencyTypeDescription { get; set; }
        public string currencyTypeName { get; set; }
        public string label { get; set; }
        public string tributaryIdentifier { get; set; }
        public string tributaryIdentifierType { get; set; }
        public string pspBankDescription { get; set; }
        public bool virtualAccount { get; set; }
    }

    // DTOs para uso interno en controladores
    public class RegistrarEntidadRequestDTO : PSPBaseResponseDTO
    {
        public string TributaryIdentifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string DocumentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int CityId { get; set; } = 3; // Default
        public int Province { get; set; } = 24; // Buenos Aires
    }

    public class RegistrarEntidadResponseDTO : PSPBaseResponseDTO
    {
        public string EntityId { get; set; }
        public int? PersonId { get; set; }
    }

    // *** DTOs PARA CREAR USUARIO ***
    public class CreateUserRequestDTO
    {
        public string userType { get; set; } = "5";           // Tipo de usuario
        public string userName { get; set; }                  // Nombre de usuario único
        public string documentType { get; set; } = "CUIL";    // Tipo de documento
        public string documentNumber { get; set; }            // Número de documento
        public string firstName { get; set; }                 // Nombre
        public string lastName { get; set; }                  // Apellido
        public string email { get; set; }                     // Email
        public string phoneNumber { get; set; }               // Teléfono
        public string address { get; set; }                   // Dirección
        public string departmentId { get; set; } = "19";      // ID departamento
        public string cityId { get; set; } = "17934";         // ID ciudad
        public bool Active { get; set; } = true;              // Usuario activo
        public List<int> roles { get; set; } = new List<int> { 9 }; // Roles (9 = viewer)
        public string password { get; set; }                  // Contraseña
        public string passwordConfirm { get; set; }           // Confirmación contraseña
    }

    public class CreateUserResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public string UserToken { get; set; }  // Token del usuario creado
        public string Error { get; set; }
    }

    // *** DTOs PARA SELF REGISTRATION ***
    public class SelfRegistrationRequestDTO
    {
        public int entityTypeId { get; set; } = 5;               // Tipo de entidad
        public int parentId { get; set; } = 1601;                // ID padre
        public bool isPhysicalPerson { get; set; } = false;      // Es persona física
        public bool taxPayer { get; set; } = false;              // Es contribuyente
        public bool isPyME { get; set; } = false;                // Es PyME
        public DateTime? PyMEEffectiveDate { get; set; }         // Fecha efectiva PyME
        public string tributaryIdentifierType { get; set; } = "CUIT"; // Tipo identificador
        public string tributaryIdentifier { get; set; }          // CUIT/CUIL
        public string name { get; set; }                         // Nombre entidad
        public string phoneCode { get; set; } = "549";           // Código teléfono
        public string phone { get; set; }                        // Teléfono
        public string address { get; set; }                      // Dirección
        public string floor { get; set; }                        // Piso
        public string department { get; set; }                   // Departamento
        public int cityId { get; set; } = 3;                     // ID ciudad
        public string postalCode { get; set; }                   // Código postal
        public string email { get; set; }                        // Email
        public bool isRevalidation { get; set; } = true;         // Es revalidación
        public bool IsSameAddress { get; set; } = true;          // Misma dirección
        public string activityPostalCode { get; set; }           // CP actividad
        public int activityCityId { get; set; } = 3;             // Ciudad actividad
        public string activityAddress { get; set; }              // Dirección actividad
        public string activityFloor { get; set; }                // Piso actividad
        public string activityDepartment { get; set; }           // Depto actividad
        public string FantasyName { get; set; }                  // Nombre fantasía
        public string cuf { get; set; }                          // CUF
        public string CovenantCode { get; set; }                 // Código convenio
    }

    public class SelfRegistrationResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Identifier { get; set; }  // ID para subir archivos después
        public string EntityId { get; set; }
        public string Error { get; set; }
    }

    // *** DTOs PARA ENDPOINTS CON UAT (FALTABAN ESTOS) ***

    // DTO para CrearUsuario con UAT
    public class CreateUserWithUATRequestDTO : CreateUserRequestDTO
    {
        public string UAT { get; set; }  // Token de autenticación del usuario administrador
    }

    public class CreateUserWithUATResponseDTO : PSPBaseResponseDTO
    {
        public int? UserId { get; set; }
        public string UserToken { get; set; }  // Token del usuario creado
        public string Identifier { get; set; }  // Token del usuario creado
        public string EntityId { get; set; }  // Token del usuario creado
    }

    // DTO para SelfRegistration con UAT
    public class SelfRegistrationWithUATRequestDTO : SelfRegistrationRequestDTO
    {
        public string UAT { get; set; }        // Token de autenticación del usuario administrador
        public string UserToken { get; set; }  // Token del usuario que ejecuta SelfRegistration
    }

    public class SelfRegistrationWithUATResponseDTO : PSPBaseResponseDTO
    {
        public string Identifier { get; set; }  // ID para subir archivos después
        public string EntityId { get; set; }
    }

    // *** DTOs PARA UPLOAD FILES ***

    // DTO para subir archivos
    public class UploadFilesRequestDTO
    {
        public string Identifier { get; set; }       // ID obtenido de SelfRegistration
        public string UserToken { get; set; }        // Token del usuario
        public Dictionary<string, byte[]> Files { get; set; } = new Dictionary<string, byte[]>();
    }

    // DTO para subir archivos con UAT (para endpoint)
    public class UploadFilesWithUATRequestDTO
    {
        public string UAT { get; set; }              // Token administrador
        public string Identifier { get; set; }       // ID obtenido de SelfRegistration  
        public string UserToken { get; set; }        // Token del usuario
        // Los archivos se manejan como IFormFile en el controlador
    }

    // DTO para respuesta de upload files
    public class UploadFilesResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> UploadedFiles { get; set; } = new List<string>();
        public string Error { get; set; }
    }

    // DTO para respuesta de upload files con UAT (para endpoint)
    public class UploadFilesWithUATResponseDTO : PSPBaseResponseDTO
    {
        public List<string> UploadedFiles { get; set; } = new List<string>();
    }

    // DTO para provincia
    public class ProvinceDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    // DTO para ciudad
    public class CityDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int provinceId { get; set; }
        public string postalCode { get; set; }
    }

    // DTO para respuesta de provincias
    public class ProvincesResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ProvinceDTO> Provinces { get; set; } = new List<ProvinceDTO>();
        public string Error { get; set; }
    }

    // DTO para respuesta de ciudades
    public class CitiesResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<CityDTO> Cities { get; set; } = new List<CityDTO>();
        public string Error { get; set; }
    }

    // DTOs para endpoints con UAT
    public class ProvincesWithUATResponseDTO : PSPBaseResponseDTO
    {
        public List<ProvinceDTO> Provinces { get; set; } = new List<ProvinceDTO>();
    }

    public class CitiesWithUATResponseDTO : PSPBaseResponseDTO
    {
        public List<CityDTO> Cities { get; set; } = new List<CityDTO>();
    }

    // *** DTOs PARA CREAR ENTIDAD Y USUARIO CON UAT ***
    public class CreateEntityAndUserWithUATRequestDTO
    {
        public string UAT { get; set; }  // Token de autenticación del usuario administrador
        public EntityDTO entity { get; set; }
        public PersonDTO person { get; set; }
    }

    public class CreateEntityAndUserWithUATResponseDTO : PSPBaseResponseDTO
    {
        public string EntityId { get; set; }
        public int? PersonId { get; set; }
    }

    public class CreateUserEntidadRequestDTO : PSPBaseResponseDTO
    {
        public SelfRegistrationRequestDTO entity { get; set; }
        public CreateUserRequestDTO user { get; set; }
        public Dictionary<string, byte[]> files { get; set; } = new Dictionary<string, byte[]>();
    }

    // *** DTOs PARA ACCOUNTS/ALL/GET ENDPOINT ***

    /// <summary>
    /// DTO para la información de cuenta del usuario (mapea respuesta del PSP)
    /// </summary>
    public class AccountInfoDTO
    {
        public string accountNumber { get; set; }
        public int accountTypeId { get; set; }
        public string tributaryIdentifierType { get; set; }
        public string tributaryIdentifier { get; set; }
        public string currencyDescription { get; set; }
        public string currencyName { get; set; }
        public string currencySymbol { get; set; }
        public int currencyTypeId { get; set; }
        public string cvU_CBU { get; set; }
        public string cvU_CBUAlias { get; set; }
        public string name { get; set; }
        public bool deleteAccountSolicitude { get; set; }
        public int entityId { get; set; }
    }

    /// <summary>
    /// DTO para la respuesta del servicio PSP (uso interno del servicio)
    /// </summary>
    public class AccountsInfoResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<AccountInfoDTO> Accounts { get; set; } = new List<AccountInfoDTO>();
        public string Error { get; set; }
    }

    /// <summary>
    /// DTO para la respuesta del controlador API (incluye UAT y campos de respuesta estándar)
    /// </summary>
    public class AccountsInfoWithUATResponseDTO : PSPBaseResponseDTO
    {
        public List<AccountInfoDTO> Accounts { get; set; } = new List<AccountInfoDTO>();
    }

    /// <summary>
    /// Request DTO para validar cuenta externa vía PSP desde endpoint interno
    /// </summary>
    public class ValidateExternalAccountRequestDTO : PSPBaseResponseDTO
    {
        public string TextSearch { get; set; }
        public string UserToken { get; set; } // optional: token del usuario PSP para hacer la validación en contexto del usuario
    }

    /// <summary>
    /// Response DTO que incluye datos de la cuenta externa (para endpoints con UAT)
    /// </summary>
    public class ExternalAccountWithUATResponseDTO : PSPBaseResponseDTO
    {
        public ExternalAccountData Data { get; set; }
    }

    // Add transaction-related DTOs
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
        public string currencyTypeId { get; set; }
        public decimal balance { get; set; }
        public int transactionTypeId { get; set; }
        public string availabilityDate { get; set; }
        public string concept { get; set; }
        public string validationCode { get; set; }
        public bool isExternal { get; set; }
        public AccountRefDTO originAccount { get; set; }
        public AccountRefDTO destinationAccount { get; set; }
    }

    public class TransactionResultDTO
    {
        public bool Success { get; set; }
        public int? TransactionId { get; set; }
        public string Message { get; set; }
        public string RawResponse { get; set; }
        public string Error { get; set; }
    }

    /// <summary>
    /// Request DTO para crear transacción con UAT
    /// </summary>
    public class TransactionWithUATRequestDTO : PSPBaseResponseDTO
    {
        public TransactionRequestDTO Transaction { get; set; }
        public string UserToken { get; set; } // Token del usuario PSP que autoriza la transferencia
    }

    /// <summary>
    /// Response DTO para crear transacción con UAT
    /// </summary>
    public class TransactionWithUATResponseDTO : PSPBaseResponseDTO
    {
        public int? TransactionId { get; set; }
        public string RawResponse { get; set; }
    }
}