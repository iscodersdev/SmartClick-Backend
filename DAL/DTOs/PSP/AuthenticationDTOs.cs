using System;
using System.Collections.Generic;

namespace DAL.DTOs.PSP
{
    // DTOs para autenticación y token
    public class TokenRequestDTO
    {
        public string grant_type { get; set; } = "password";
        public string username { get; set; }
        public string password { get; set; }
        public string client_secret { get; set; }
        public string client_id { get; set; }
    }

    public class TokenResponseDTO
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }

    // DTOs para crear entidad y usuario
    public class CreateEntityUserRequestDTO
    {
        public EntityDTO entity { get; set; }
        public PersonDTO person { get; set; }
    }

    public class EntityDTO
    {
        public int entityTypeId { get; set; } = 5; // Persona Física
        public int parentId { get; set; } = 1601;
        public int? commercialPlanId { get; set; }
        public bool isPhysicalPerson { get; set; } = false;
        public bool? taxPayer { get; set; }
        public bool IsPyME { get; set; } = false;
        public DateTime? PyMEEffectiveDate { get; set; }
        public string tributaryIdentifierType { get; set; } = "CUIT";
        public string tributaryIdentifier { get; set; }
        public string name { get; set; }
        public string FantasyName { get; set; }
        public string CUF { get; set; }
        public string CovenantCode { get; set; }
        public int singleTransactionApprovalStages { get; set; } = 0;
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int cityId { get; set; }
        public string floor { get; set; }
        public string department { get; set; }
        public string postalCode { get; set; }
        public bool IsSameAddress { get; set; } = true;
        public string activityAddress { get; set; }
        public int? activityProvince { get; set; }
        public int activityCityId { get; set; }
        public string activityFloor { get; set; }
        public string activityDepartment { get; set; }
        public string activityPostalCode { get; set; }
        public string CreateUser { get; set; }
        public List<object> files { get; set; } = new List<object>();
        public string phoneCode { get; set; } = "549";
    }

    public class PersonDTO
    {
        public string documentType { get; set; } = "CUIL";
        public string documentNumber { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int cityId { get; set; }
        public int province { get; set; }
        public string email { get; set; }
        public string phoneCode { get; set; } = "549";
        public List<int> roles { get; set; } = new List<int> { 9 }; // TransactionViewer
    }

    public class CreateEntityUserResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? EntityId { get; set; }
        public int? PersonId { get; set; }
        public string Error { get; set; }
    }

    // DTOs base para respuestas de API
    public class PSPBaseResponseDTO
    {
        public int Status { get; set; }
        public string UAT { get; set; }
        public string Mensaje { get; set; }
        public bool Success { get; set; }
    }

    // DTOs para uso interno en controladores
    public class RegistrarEntidadRequestDTO : PSPBaseResponseDTO
    {
        public string TributaryIdentifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string DocumentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int CityId { get; set; } = 3; // Default
        public int Province { get; set; } = 24; // Buenos Aires
    }

    public class RegistrarEntidadResponseDTO : PSPBaseResponseDTO
    {
        public int? EntityId { get; set; }
        public int? PersonId { get; set; }
    }
}