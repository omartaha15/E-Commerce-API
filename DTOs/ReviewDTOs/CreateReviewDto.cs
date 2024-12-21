using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.ReviewDTOs
{
    public class CreateReviewDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }
    }
}
