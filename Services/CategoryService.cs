using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs.CategoryDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService ( ApplicationDbContext dbContext, IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> CreateCategoryAsync ( CreateCategoryDTO categoryDTO )
        {
            var catefory = _mapper.Map<Category>( categoryDTO );

            _dbContext.Categories.Add( catefory );
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>( categoryDTO );
        }

        public async Task DeleteCategoryAsync ( int id )
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync( c => c.Id == id);

            if(category == null )
            {
                throw new KeyNotFoundException("Category not Exist");
            }

            _dbContext.Categories.Remove( category );
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CategoryDTO>> GetAllAsync ()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>( categories );    
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync ( int id )
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync ( c => c.Id == id);
            if( category == null )
            {
                throw new KeyNotFoundException( "Category not Exist" );
            }

            return _mapper.Map<CategoryDTO> ( category );
        }

        public async Task<List<CategoryDTO>> SearchCategoryAsync ( string query )
        {
            var category = await _dbContext.Categories
                .Where(c => c.Name.Contains(query))
                .ToListAsync();

            return _mapper.Map<List<CategoryDTO>>( category );
        }

        public async Task<CategoryDTO> UpdateCategoryAsync ( int id, UpdateCategoryDTO categoryDTO )
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync( c => c.Id == id);

            if( category == null )
            {
                throw new KeyNotFoundException( "Category not Exist" );
            }

            _mapper.Map( categoryDTO, category );

            await _dbContext.SaveChangesAsync();  

            return _mapper.Map<CategoryDTO>( category );
        }
    }
}
