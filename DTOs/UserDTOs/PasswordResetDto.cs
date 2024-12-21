using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.UserDTOs
{
    public class PasswordResetDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength( 100, MinimumLength = 6 )]
        public string NewPassword { get; set; }

        [Required]
        [Compare( nameof( NewPassword ) )]
        public string ConfirmNewPassword { get; set; }
    }
}
