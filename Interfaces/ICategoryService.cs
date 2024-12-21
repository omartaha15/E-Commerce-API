using E_Commerce_API.DTOs.CategoryDTOs;
using E_Commerce_API.DTOs.ProductDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync ();
        Task<CategoryDTO> GetCategoryByIdAsync ( int id );
        Task<CategoryDTO> CreateCategoryAsync ( CreateCategoryDTO categoryDTO );
        Task<CategoryDTO> UpdateCategoryAsync ( int id, UpdateCategoryDTO categoryDTO );
        Task DeleteCategoryAsync ( int id );
        Task<List<CategoryDTO>> SearchCategoryAsync ( string query );
    }
}
