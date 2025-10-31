using Newtonsoft.Json;

namespace DAL.DTOs.PSP
{
    public class RecoverPasswordRequestDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class ResetPasswordRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string EventValidator { get; set; }
    }

    public class SimplePspResponseDTO
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string code { get; set; }
        public int? httpStatusCode { get; set; }
    }

    // DTOs that include UAT for controller requests
    public class RecoverPasswordWithUATRequestDTO : RecoverPasswordRequestDTO
    {
        public string UAT { get; set; }
    }

    public class ResetPasswordWithUATRequestDTO : ResetPasswordRequestDTO
    {
        public string UAT { get; set; }
    }
}
