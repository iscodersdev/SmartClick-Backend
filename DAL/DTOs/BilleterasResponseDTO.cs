using System.Collections.Generic;

namespace DAL.DTOs
{
    public class BilleterasResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public List<BilleteraDTO> Billeteras { get; set; }
    }

}