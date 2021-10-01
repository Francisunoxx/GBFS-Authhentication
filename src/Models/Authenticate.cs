using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi
{
    public class Authenticate
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}