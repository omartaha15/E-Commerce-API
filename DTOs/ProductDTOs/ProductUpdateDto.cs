using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.ProductDTOs
{
    public class ProductUpdateDto
    {
        [Required]
        [StringLength( 200 )]
        public string Name { get; set; }

        [StringLength( 1000 )]
        public string? Description { get; set; }

        [Range( 0.01, double.MaxValue )]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Range( 0, int.MaxValue )]
        public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CategoryId { get; set; }
    }
}
