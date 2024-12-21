using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.UserDTOs
{
    public class UpdateProfileDto
    {
        [StringLength( 100 )]
        public string? FirstName { get; set; }

        [StringLength( 100 )]
        public string? LastName { get; set; }

        [StringLength( 200 )]
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
