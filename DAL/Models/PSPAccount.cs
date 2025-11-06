using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class PSPAccount
    {
        public int Id { get; set; }
        public string RequestId { get; set; } // para idempotencia / reintentos

        // --- Datos Locales ---
        public int? ClienteId { get; set; }
        public string UsuarioId { get; set; } // matches AspNetUsers.Id (string)
        public string TributaryIdentifier { get; set; } // CUIL
        public string UserName { get; set; } // Email

        // --- Datos del PSP ---
        public string PSPUserId { get; set; }
        public string EntityId { get; set; }        // EntityId del PSP (string to allow alphanumeric)
        public string Identifier { get; set; }    // Identifier de SelfRegistration
        public string AccountNumber { get; set; }
        public string CVU { get; set; }
        public string CVU_CBUAlias { get; set; }
        public int? AccountTypeId { get; set; }
        public string TributaryIdentifierType { get; set; }
        public string CurrencyDescription { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public int? CurrencyTypeId { get; set; }
        public bool? DeleteAccountSolicitude { get; set; }

        // Optional fields from Children/Get
        public int? EntityStatus { get; set; }
        public string EntityStatusDescription { get; set; }
        public string StatusDescription { get; set; }

        // --- Estado y Tokens ---
        public string Status { get; set; }        // creating, user_created, registered, active, pending_approval, error_*
        public string ErrorMessage { get; set; }
        [Column(TypeName = "text")]
        public string EncryptedUserToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
        [Column(TypeName = "text")]
        public string EncryptedPassword { get; set; } // Para refrescar el token si es necesario

        // --- Campos de Trazabilidad ---
        [Column(TypeName = "text")]
        public string LastC1ResponseJson { get; set; }
        [Column(TypeName = "text")]
        public string LastC7ResponseJson { get; set; }
        public DateTime? LastStatusCheck { get; set; }


        // --- Timestamps ---
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // --- Relaciones (Opcional pero recomendado) ---
        public virtual Clientes Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual PSPAccountStatus EstadoCuentaPSP { get; set; }
        public virtual ICollection<PSPAccountFile> PSPAccountFiles { get; set; } = new List<PSPAccountFile>();
    }

    public class PSPAccountStatus
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Aceptado { get; set; }
    }
    
}
