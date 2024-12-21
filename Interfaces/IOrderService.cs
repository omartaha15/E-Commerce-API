using E_Commerce_API.DTOs.OrderDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync ( OrderCreateDto orderDto ) ;
        Task<OrderDetailsDto> GetOrderByIdAsync ( int id );
        Task UpdateOrderAsync ( int orderId, OrderUpdateDto orderUpdateDto );
        Task<List<AdminOrderListDto>> GetAllOrdersAsync ();
        Task<List<OrderDetailsDto>> GetUserOrdersAsync ( string userId );
        Task CancelOrderAsync ( int id );
        Task UpdateOrderStatusAsync ( int id, string status );
    }
}
