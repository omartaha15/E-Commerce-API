using E_Commerce_API.DTOs.ProductDTOs;

namespace E_Commerce_API.DTOs.OrderDTOs
{
    public class OrderItemDetailsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
