using System.Threading.Tasks;
using DAL.DTOs.PSP;

namespace BusinessCore.Services
{
    public interface IPSPService
    {
        /// <summary>
        /// Obtiene un token de acceso del PSP
        /// </summary>
        Task<TokenResponseDTO> GetAccessTokenAsync();

        /// <summary>
        /// Crea una nueva entidad y usuario en el PSP
        /// </summary>
        Task<CreateEntityUserResponseDTO> CreateEntityAndUserAsync(CreateEntityUserRequestDTO request);

        /// <summary>
        /// Registra una entidad completa usando datos simplificados
        /// </summary>
        Task<RegistrarEntidadResponseDTO> RegistrarEntidadAsync(RegistrarEntidadRequestDTO request);

        /// <summary>
        /// Valida si las credenciales del PSP están configuradas correctamente
        /// </summary>
        bool ValidateConfiguration();

        /// <summary>
        /// Crea un nuevo usuario en el PSP
        /// </summary>
        Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO request);

        /// <summary>
        /// Crea una entidad asociada al usuario autenticado (SelfRegistration)
        /// </summary>
        Task<SelfRegistrationResponseDTO> SelfRegistrationAsync(SelfRegistrationRequestDTO request, string userToken);

        /// <summary>
        /// Sube archivos de validación para una entidad
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
        /// Indica si el servicio está en modo de prueba
        /// </summary>
        bool IsTestMode();
    }
}