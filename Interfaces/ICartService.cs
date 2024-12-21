using E_Commerce_API.DTOs.CartDTOs;
using E_Commerce_API.Model;

namespace E_Commerce_API.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItems ( string userId );
        Task AddToCart ( string userId, AddCartItemDto cartItemDto );
        Task UpdateCartItem ( string userId, UpdateCartItemDto cartItemDto );
        Task RemoveCartItem ( string userId, int productId );
        Task ClearCart ( string userId );
    }
}
