using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi
{
    public class ForgotPasswordForm
    {
        [Required]
        public string Email { get; set; }
    }
}
