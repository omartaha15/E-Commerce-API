using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.OrderDTOs
{
    public class OrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range( 1, int.MaxValue )]
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
