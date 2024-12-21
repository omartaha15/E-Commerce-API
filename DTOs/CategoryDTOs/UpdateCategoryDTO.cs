using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [StringLength( 500 )]
        public string? Description { get; set; }
    }
}
