using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs.ProductDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public WishlistService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> AddProductToWishlistAsync ( string userId, int productId )
        {
            var product = await _dbContext.Products.FindAsync( productId );
            if ( product == null )
                return false;

            var alreadyExists = await _dbContext.WishlistItems
                .AnyAsync( w => w.UserId == userId && w.ProductId == productId );

            if ( alreadyExists )
                return false;

            var wishlistItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId
            };

            _dbContext.WishlistItems.Add( wishlistItem );
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetUserWishlistAsync ( string userId )
        {
            var wishlistItems = await _dbContext.WishlistItems
                .Where( w => w.UserId == userId )
                .Include( w => w.Product )
                .ToListAsync();

            var products = wishlistItems.Select( w => w.Product );
            return _mapper.Map<IEnumerable<ProductDto>>( products );
        }

        public async Task<bool> RemoveProductFromWishlistAsync ( string userId, int productId )
        {
            var wishlistItem = await _dbContext.WishlistItems
                 .FirstOrDefaultAsync( w => w.UserId == userId && w.ProductId == productId );

            if ( wishlistItem == null )
                return false;

            _dbContext.WishlistItems.Remove( wishlistItem );
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
