using E_Commerce_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController ( IWishlistService wishlistService )
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        [ProducesResponseType( StatusCodes.Status401Unauthorized )]
        public async Task<ActionResult> GetUserWishlist ()
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( userId == null )
                {
                    return Unauthorized();
                }

                var wishlist = await _wishlistService.GetUserWishlistAsync( userId );
                return Ok( wishlist );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPost( "add/{productId}" )]
        [Authorize]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status401Unauthorized )]
        public async Task<ActionResult> AddProductToWishList ( int productId )
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( userId == null )
                {
                    return Unauthorized();
                }

                var success = await _wishlistService.AddProductToWishlistAsync( userId, productId );

                if ( !success )
                    return BadRequest( new { message = "Product could not be added to the wishlist. It may already be in the wishlist or does not exist." } );

                return Ok( new { message = "Product added to wishlist successfully." } );


            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpDelete("remove/{productId}")]
        [Authorize]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status401Unauthorized )]
        public async Task<ActionResult> RemoveProductFromWishList(int productId )
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( userId == null )
                {
                    return Unauthorized();
                }

                var success = await _wishlistService.RemoveProductFromWishlistAsync( userId, productId );
                if ( !success )
                    return NotFound( new { message = "Product not found in wishlist or does not exist." } );

                return Ok( new { message = "Product removed from wishlist successfully." } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }

        }
    }
}
