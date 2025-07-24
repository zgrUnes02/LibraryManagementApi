using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApi.DTOs.Authentication
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        [DefaultValue("Admin")]
        public string Role { get; set; }
    }
}
