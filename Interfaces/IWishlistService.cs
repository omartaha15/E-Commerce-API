using E_Commerce_API.DTOs.ProductDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface IWishlistService
    {
        Task<IEnumerable<ProductDto>> GetUserWishlistAsync ( string userId );
        Task<bool> AddProductToWishlistAsync ( string userId, int productId );
        Task<bool> RemoveProductFromWishlistAsync ( string userId, int productId );
    }
}
