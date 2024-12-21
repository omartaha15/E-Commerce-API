using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs.OrderDTOs
{
    public class OrderCreateDto
    {
        public string UserId { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }
}
