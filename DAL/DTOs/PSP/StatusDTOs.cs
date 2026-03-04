using System.Collections.Generic;
using DAL.Models;
using Newtonsoft.Json;

namespace DAL.DTOs.PSP
{
    // DTO para el request del endpoint /status
    public class PSPStatusRequestDTO
    {
        public int? UsuarioId { get; set; }
        public string Cuil { get; set; }
        public string UAT { get; set; }
        public string UserToken { get; set; }
    }

    // DTO para la respuesta del endpoint /status
    public class PSPStatusResponseDTO
    {
        public bool Success { get; set; }
        public PSPAccountStatus Estado { get; set; } // "crear_cuenta", "espera", "activa", "error"
        public string Mensaje { get; set; }
        public string EntityId { get; set; }
        public string Cvu { get; set; }
    }

    // --- DTOs para la respuesta del endpoint C7 ---
    public class EntityStatusResponseDTO : PSPBaseResponseDTO
    {
        [JsonProperty("data")]
        public List<EntityStatusData> Data { get; set; }
        
        public string Error { get; set; }
        public string RawResponse { get; set; }
    }

    public class EntityStatusData
    {
        public string EntityName { get; set; }
        public int EntityStatus { get; set; }
        public string EntityStatusDescription { get; set; }
        public List<EntityAccountStatus> Accounts { get; set; }
    }

    public class EntityAccountStatus
    {
        public string AccountNumber { get; set; }
        [JsonProperty("cvU_CBU")]
        public string Cvu { get; set; }
        public int Status { get; set; }
        public string StatusDescription { get; set; }
        public int EntityId { get; set; }
    }
}
