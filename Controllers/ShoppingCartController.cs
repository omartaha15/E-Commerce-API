using E_Commerce_API.DTOs.CartDTOs;
using E_Commerce_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public ShoppingCartController ( ICartService cartService )
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart ()
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier ); 
                if ( string.IsNullOrEmpty( userId ) )
                    return Unauthorized( new { message = "User not authenticated" } );

                var cartItems = await _cartService.GetCartItems( userId );
                return Ok( cartItems );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPost( "add" )]
        public async Task<IActionResult> AddToCart ( [FromBody] AddCartItemDto cartItemDto )
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( string.IsNullOrEmpty( userId ) )
                    return Unauthorized( new { message = "User not authenticated" } );

                await _cartService.AddToCart( userId, cartItemDto );
                return Ok( new { message = "Item added to cart" } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPut( "update" )]
        public async Task<IActionResult> UpdateCartItem ( [FromBody] UpdateCartItemDto cartItemDto )
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( string.IsNullOrEmpty( userId ) )
                    return Unauthorized( new { message = "User not authenticated" } );

                await _cartService.UpdateCartItem( userId, cartItemDto );
                return Ok( new { message = "Cart item updated" } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpDelete( "remove/{productId}" )]
        public async Task<IActionResult> RemoveCartItem ( int productId )
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( string.IsNullOrEmpty( userId ) )
                    return Unauthorized( new { message = "User not authenticated" } );

                await _cartService.RemoveCartItem( userId, productId );
                return Ok( new { message = "Item removed from cart" } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPost( "clear" )]
        public async Task<IActionResult> ClearCart ()
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( string.IsNullOrEmpty( userId ) )
                    return Unauthorized( new { message = "User not authenticated" } );

                await _cartService.ClearCart( userId );
                return Ok( new { message = "Cart cleared" } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }
    }
}
