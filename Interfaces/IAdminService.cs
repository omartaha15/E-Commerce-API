using E_Commerce_API.DTOs;
using E_Commerce_API.DTOs.OrderDTOs;
using E_Commerce_API.DTOs.ProductDTOs;
using E_Commerce_API.DTOs.UserDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface IAdminService
    {
        Task<SalesSummaryDto> GetSalesSummaryAsync();

        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<ProductDto>> GetLowStockProductsAsync ();
        Task<IEnumerable<OrderDto>> GetRecentOrdersAsync ();
    }
}
