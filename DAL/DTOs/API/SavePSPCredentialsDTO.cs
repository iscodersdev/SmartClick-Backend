using DAL.DTOs.PSP;

namespace DAL.DTOs.API
{
    /// <summary>
    /// Request DTO para guardar credenciales PSP de un usuario
    /// </summary>
    public class SavePSPCredentialsRequestDTO : PSPBaseResponseDTO
    {
        /// <summary>
        /// Username del PSP (puede ser diferente del email local)
        /// </summary>
        public string PSPUsername { get; set; }

        /// <summary>
        /// Password del PSP (se guardará cifrado)
        /// </summary>
        public string PSPPassword { get; set; }
    }

    /// <summary>
    /// Response DTO para guardar credenciales PSP
    /// </summary>
    public class SavePSPCredentialsResponseDTO : PSPBaseResponseDTO
    {
        /// <summary>
        /// Indica si el token fue generado exitosamente tras validar credenciales
        /// </summary>
        public bool TokenGenerated { get; set; }

        /// <summary>
        /// Fecha de expiración del token (si se generó)
        /// </summary>
        public System.DateTime? TokenExpiry { get; set; }
    }
}
