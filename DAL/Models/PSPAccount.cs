using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class PSPAccount
    {
        public int Id { get; set; }

        // FK opcionales hacia tus entidades locales (no obliga a cambiar tablas existentes)
        public int? ClienteId { get; set; }
        public int? UsuarioId { get; set; }

        // Identificadores devueltos por PSP
        public string PSPUserId { get; set; }
        public string UserName { get; set; }
        public string Identifier { get; set; }    // Identifier de SelfRegistration
        public int? EntityId { get; set; }        // EntityId del PSP
        public string AccountNumber { get; set; } // CVU/CBU si aplica

        // Token cifrado y metadatos
        public string EncryptedUserToken { get; set; }
        public DateTime? TokenExpiry { get; set; }

        // Control de estado / idempotencia / auditoría
        public string Status { get; set; }        // creating, user_created, registered, files_uploaded, validated, error...
        public string ErrorMessage { get; set; }
        public string RequestId { get; set; }     // para idempotencia / reintentos

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Optional: store the tributary identifier (CUIL/CUIT)
        public string TributaryIdentifier { get; set; }

        // Agregar esta colección para navegación inversa
        public virtual ICollection<PSPAccountFile> PSPAccountFiles { get; set; }
    }
}