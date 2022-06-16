using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationApi.Model.ViewModels
{
    public class UserVM
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The Password must be at least {2} characters long", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirm password doesnot match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; } = string.Empty;

        [Required]
        public int UserAge { get; set; }
    }
}
