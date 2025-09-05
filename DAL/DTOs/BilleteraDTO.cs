using DAL.Models;

namespace DAL.DTOs
{
    
    public class BilleteraDTO
    {
        public int Id { get; set; }
        public Clientes Cliente { get; set; }
        public decimal Saldo { get; set; }
        public string CVU {  get; set; }
    }
}