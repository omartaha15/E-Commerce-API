using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.UserDTOs
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength( 100, MinimumLength = 6 )]
        public string Password { get; set; }

        [Required]
        [Compare( nameof( Password ) )]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength( 100 )]
        public string FirstName { get; set; }

        [Required]
        [StringLength( 100 )]
        public string LastName { get; set; }

        [StringLength( 200 )]
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
