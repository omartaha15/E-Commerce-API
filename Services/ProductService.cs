using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs.ProductDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerce_API.Services
{
    public class ProductService :  IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext dbContext , IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync ( ProductCreateDto productDto )
        {
            var product = _mapper.Map<Product>( productDto );
            _dbContext.Products.Add( product );
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>( product );
        }



        public async Task DeleteProductAsync ( int id )
        {
            var product = _dbContext.Products.FirstOrDefault( p => p.Id == id );

            if ( product == null )
            {
                throw new KeyNotFoundException( "Product not found." );
            }

            _dbContext.Products.Remove( product );
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAllAsync ()
        {
            var products = await _dbContext.Products.ToListAsync();

            return _mapper.Map<List<ProductDto>>( products );
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync ( int id )
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync( p => p.Id == id );

            if ( product == null )
            {
                throw new KeyNotFoundException( "Product not found." );
            }

            return _mapper.Map<ProductDetailsDto>( product );
        }

        public async Task<(List<ProductDto> Products, int TotalCount)> GetProductsAsync ( ProductSearchDto searchDto )
        {
           var products = _dbContext.Products.Include(p => p.Category).AsQueryable();

            if(!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                products = products.Where( p => p.Name.Contains( searchDto.SearchTerm ) );
            }

            if(searchDto.CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == searchDto.CategoryId);
            }

            if ( searchDto.MinPrice.HasValue )
                products = products.Where( p => p.Price >= searchDto.MinPrice.Value );

            if ( searchDto.MaxPrice.HasValue )
                products = products.Where( p => p.Price <= searchDto.MaxPrice.Value );

            var totalCount = await products.CountAsync();

            var productsPages = await products
                .Skip( (searchDto.Page - 1 ) * searchDto.PageSize )
                .Take( searchDto.PageSize )
                .ToListAsync();

            return (_mapper.Map<List<ProductDto>>( products ), totalCount);
        }

        public async Task<List<ProductDto>> SearchProductsAsync ( string query )
        {
            var products = await _dbContext.Products
                .Where( p => p.Name.Contains( query ) )
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>( products );
        }

        public async Task<ProductDto> UpdateProductAsync ( int id, ProductUpdateDto productDto )
        {
            var product = _dbContext.Products.FirstOrDefault( p => p.Id == id );
            if ( product == null )
            {
                throw new KeyNotFoundException( "Product not found." );
            }

            _mapper.Map( productDto, product );
            await _dbContext.SaveChangesAsync(); 

            return _mapper.Map<ProductDto>( product );
        }
    }
}
