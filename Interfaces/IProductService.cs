using E_Commerce_API.DTOs.ProductDTOs;
using E_Commerce_API.Model;

namespace E_Commerce_API.Interfaces
{
    public interface IProductService
    {

        Task<List<ProductDto>> GetAllAsync();
        Task<(List<ProductDto> Products, int TotalCount)> GetProductsAsync ( ProductSearchDto searchDto );
        Task<ProductDetailsDto> GetProductByIdAsync ( int id );
        Task<ProductDto> CreateProductAsync ( ProductCreateDto productDto );
        Task<ProductDto> UpdateProductAsync ( int id, ProductUpdateDto productDto );
        Task DeleteProductAsync ( int id );
        Task<List<ProductDto>> SearchProductsAsync ( string query );
    }
}
