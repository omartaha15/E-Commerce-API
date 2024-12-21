using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs.CartDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CartService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddToCart ( string userId, AddCartItemDto cartItemDto )
        {
            if ( string.IsNullOrEmpty( userId ) )
                throw new ArgumentException( "UserId cannot be null or empty" );

            if ( cartItemDto.Quantity <= 0 )
                throw new ArgumentException( "Quantity must be greater than zero" );

            if ( cartItemDto.ProductId <= 0 ) 
                throw new ArgumentException( "ProductId must be valid" );

            var exsitingCartItem = await _dbContext.ShoppingCartItems
                .AsTracking()
                .FirstOrDefaultAsync( c => c.UserId == userId && c.ProductId == cartItemDto.ProductId );

            if ( exsitingCartItem != null )
            {
                exsitingCartItem.Quantity += cartItemDto.Quantity;
            }
            else
            {
                var newCartItem = _mapper.Map<CartItem>( cartItemDto );
                newCartItem.UserId = userId; 
                _dbContext.ShoppingCartItems.Add( newCartItem );
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch ( Exception ex )
            {
                throw new Exception( "An error occurred while saving the cart item", ex );
            }
        }


        public async Task ClearCart ( string userId )
        {
            var cartItems = await _dbContext.ShoppingCartItems.Where( c => c.UserId == userId ).ToListAsync();

            _dbContext.ShoppingCartItems.RemoveRange( cartItems );
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCartItems ( string userId )
        {
            var cartItems = await _dbContext.ShoppingCartItems.Where( c => c.UserId == userId ).ToListAsync();
            return cartItems;
        }

        public async Task RemoveCartItem ( string userId, int productId )
        {
            var cartItem = await _dbContext.ShoppingCartItems
                .FirstOrDefaultAsync( c => c.UserId == userId && c.ProductId == productId );

            if ( cartItem == null )
            {
                throw new Exception( "Item not found in cart." );
            }

            _dbContext.ShoppingCartItems.Remove( cartItem );
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCartItem ( string userId, UpdateCartItemDto cartItemDto )
        {
            var cartItem = await _dbContext.ShoppingCartItems
                .FirstOrDefaultAsync( c => c.UserId == userId && c.ProductId == cartItemDto.ProductId );

            if ( cartItem == null )
            {
                throw new Exception( "Item not found in cart." );
            }

            cartItem.Quantity = cartItemDto.Quantity;

            await _dbContext.SaveChangesAsync();
        }
    }
}
