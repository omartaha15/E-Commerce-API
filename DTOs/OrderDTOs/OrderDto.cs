namespace E_Commerce_API.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
