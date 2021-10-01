using System;

namespace AuthenticationApi
{
    public class Information
    {
        public int Id { get; set; }
        public int BorrowerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool HasReset { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? Expiry { get; set; }
    }
}