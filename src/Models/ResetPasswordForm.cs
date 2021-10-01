using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi
{
    public class ResetPasswordForm
    {
        [Required]
        public string password { get; set; }
        [Required]
        public string token { get; set; }
    }
}
