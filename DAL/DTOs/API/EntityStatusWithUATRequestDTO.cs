using DAL.DTOs.PSP;

namespace DAL.DTOs.API
{
    /// <summary>
    /// DTO para la solicitud de consulta de estado de entidad con UAT
    /// </summary>
    public class EntityStatusWithUATRequestDTO : PSPBaseResponseDTO
    {
        /// <summary>
        /// Identificador tributario (CUIT/CUIL) de la entidad a consultar.
        /// </summary>
        public string TributaryIdentifier { get; set; }
    }
}
