using System.Threading.Tasks;
using DAL.DTOs.PSP;
using System.Collections.Generic;

namespace BusinessCore.Services
{
    public interface IPSPService
    {
        /// <summary>
        /// Obtiene un token de acceso del PSP
        /// </summary>
        Task<TokenResponseDTO> GetAccessTokenAsync();


        /// <summary>
        /// Obtiene un token de acceso del PSP de un Cliente
        /// </summary>
        Task<TokenResponseDTO> GetAccessTokenUserAsync(string username, string paswword);

        /// <summary>
        /// Crea una nueva entidad y usuario en el PSP
        /// </summary>
        //Task<CreateEntityUserResponseDTO> CreateEntityAndUserAsync(CreateEntityUserRequestDTO request);

        /// <summary>
        /// Registra una entidad completa usando datos simplificados
        /// </summary>
        //Task<RegistrarEntidadResponseDTO> RegistrarEntidadAsync(RegistrarEntidadRequestDTO request);

        /// <summary>
        /// Valida si las credenciales del PSP están configuradas correctamente
        /// </summary>
        bool ValidateConfiguration();

        /// <summary>
        /// Verifica si está en modo de prueba/mock
        /// </summary>
        bool IsTestMode();

        /// <summary>
        /// Crea un nuevo usuario en el PSP
        /// </summary>
        Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO request);

        /// <summary>
        /// Crea una entidad asociada al usuario autenticado (SelfRegistration)
        /// </summary>
        Task<SelfRegistrationResponseDTO> SelfRegistrationAsync(SelfRegistrationRequestDTO request, string userToken);

        /// <summary>
        /// Sube archivos de validación para una entidad (DNI, selfie, etc.)
        /// </summary>
        Task<UploadFilesResponseDTO> UploadFilesAsync(string identifier, string userToken, Dictionary<string, byte[]> files);

        /// <summary>
        /// Obtiene la lista de provincias disponibles
        /// </summary>
        Task<ProvincesResponseDTO> GetProvincesAsync();

        /// <summary>
        /// Obtiene la lista de ciudades de una provincia específica
        /// </summary>
        Task<CitiesResponseDTO> GetCitiesAsync(int provinceId);

        /// <summary>
        /// C1: Consulta los datos de la cuenta del usuario logueado.
        /// </summary>
        Task<AccountsInfoResponseDTO> GetAccountsInfoAsync(string userToken);

        /// <summary>
        /// Valida una cuenta externa (alias/CVU/CBU) y devuelve datos del titular (tributaryIdentifier)
        /// Se puede pasar userToken (opcional) para que la validacion se haga en contexto del usuario.
        /// </summary>
        Task<ExternalAccountLookupResponseDTO> ValidateExternalAccountAsync(string textSearch, string userToken = null);

        /// <summary>
        /// Crea una transacción (transferencia) en el PSP. El userToken debe ser el token del usuario que autoriza la transferencia.
        /// Se puede pasar localCuit para evitar consultar las cuentas del PSP cuando ya se validó localmente.
        /// </summary>
        Task<TransactionResultDTO> CreateTransactionAsync(TransactionRequestDTO request, string userToken, string localCuit = null);

        /// <summary>
        /// C1: Consulta los datos de la cuenta del usuario logueado.
        /// </summary>
        Task<AccountsInfoResponseDTO> GetAccountDataAsync(string userToken);

        /// <summary>
        /// C7: Obtiene la entidad hija por su identificador tributario.
        /// </summary>
        Task<EntityStatusResponseDTO> GetEntityByTributaryIdAsync(string tributaryIdentifier, string systemToken);
    }
}
