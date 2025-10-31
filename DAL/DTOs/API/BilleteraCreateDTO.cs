using System;

namespace DAL.DTOs.API
{
    /// <summary>
    /// DTO para crear billetera con CUIL
    /// </summary>
    public class CreateBilleteraWithCUILRequestDTO
    {
        public string UAT { get; set; }
        public string CUIL { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para crear billetera con CUIL
    /// </summary>
    public class CreateBilleteraWithCUILResponseDTO : DAL.Models.RespuestaAPI
    {
        public int? BilleteraId { get; set; }
        public string CVU { get; set; }
        public string AliasCVU { get; set; }
        public decimal Saldo { get; set; }
        public int? ClienteId { get; set; }
        public string CUIL { get; set; }
        public bool Success { get; set; }
    }

    /// <summary>
    /// DTO para editar billetera
    /// </summary>
    public class EditBilleteraRequestDTO
    {
        public string UAT { get; set; }
        public int BilleteraId { get; set; }
        public string AliasCVU { get; set; }
        public string CUIL { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para editar billetera
    /// </summary>
    public class EditBilleteraResponseDTO : DAL.Models.RespuestaAPI
    {
        public int? BilleteraId { get; set; }
        public string CVU { get; set; }
        public string AliasCVU { get; set; }
        public decimal Saldo { get; set; }
        public int? ClienteId { get; set; }
        public string CUIL { get; set; }
        public bool Success { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para información de billetera
    /// </summary>
    public class BilleteraInfoResponseDTO : DAL.Models.RespuestaAPI
    {
        public int? BilleteraId { get; set; }
        public string CVU { get; set; }
        public string AliasCVU { get; set; }
        public decimal Saldo { get; set; }
        public int? ClienteId { get; set; }
        public string CUIL { get; set; }
        public string NombreCompleto { get; set; }
        public bool Success { get; set; }
    }
}