using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.CartDTOs
{
    public class CartItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
