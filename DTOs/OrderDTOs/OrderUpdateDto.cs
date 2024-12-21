namespace E_Commerce_API.DTOs.OrderDTOs
{
    public class OrderUpdateDto
    {
        public string ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
