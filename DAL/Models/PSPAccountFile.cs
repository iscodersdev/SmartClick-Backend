using System;

namespace DAL.Models
{
    public class PSPAccountFile
    {
        public int Id { get; set; }
        public int PSPAccountId { get; set; }
        public string FileKey { get; set; }    // DNI_FRENTE, DNI_DORSO, SELFIE, INSCRIPCION_AFIP
        public string FileName { get; set; }   // nombre original
        public string StoragePath { get; set; } // ruta/URL a blob o filesystem
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // navegación opcional
        public PSPAccount PSPAccount { get; set; }
    }
}