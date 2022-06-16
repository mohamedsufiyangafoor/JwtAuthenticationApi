using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationApi.Model
{
    public class UserDetail
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public int UserAge { get; set; }
    }
}
