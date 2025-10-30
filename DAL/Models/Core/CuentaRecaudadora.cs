using Commons.Identity;
using DAL.Models.Core;
using System.Collections.Generic;

namespace DAL.Models
{
    public class CuentasRecaudadoras
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TributaryIdentifierType { get; set; }
        public string TributaryIdentifier { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseUrl { get; set; }
        public bool Activo { get; set; }     

    }

}