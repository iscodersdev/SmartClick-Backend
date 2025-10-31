using DAL.DTOs.PSP;
using System.Collections.Generic;

namespace DAL.DTOs.API
{
    /// <summary>
    /// DTO para la respuesta de consulta de estado de entidad con UAT
    /// </summary>
    public class EntityStatusWithUATResponseDTO : PSPBaseResponseDTO
    {
        /// <summary>
        /// Lista de datos de la entidad y sus cuentas
        /// </summary>
        public List<EntityStatusData> Data { get; set; }

        /// <summary>
        /// Respuesta cruda del servicio PSP para debugging
        /// </summary>
        public string RawResponse { get; set; }
    }
}
