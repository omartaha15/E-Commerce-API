namespace E_Commerce_API.DTOs.OrderDTOs
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public string TrackingNumber { get; set; }
        public List<OrderItemDetailsDto> OrderItems { get; set; }
    }

}
