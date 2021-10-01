using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi
{
    public class VerifyResetPwTokenForm
    { 
        [Required]
        public string token { get; set; }
    }
}
