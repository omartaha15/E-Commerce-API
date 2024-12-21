namespace E_Commerce_API.DTOs.OrderDTOs
{
    public class AdminOrderListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
    }
}
