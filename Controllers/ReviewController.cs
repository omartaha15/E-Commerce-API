using E_Commerce_API.DTOs.ReviewDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviews;

        public ReviewController(IReviewService reviews)
        {
            _reviews = reviews;
        }

        [HttpGet]
        public async Task<ActionResult> GetProductReviews(int productId )
        {
            try
            {
                var reviews = await _reviews.GetReviewsByProductIdAsync( productId );

                return Ok( reviews );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(int productId, [FromBody]CreateReviewDto reviewDto)
        {
            try
            {
                if ( productId != reviewDto.ProductId )
                {
                    return BadRequest( "Product ID mismatch." );
                }

                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( userId == null )
                {
                    return Unauthorized();
                }

                await _reviews.AddReviewAsync( productId, reviewDto, userId );
                return Ok();

            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult> UpdateReview(int reviewId, [FromBody] CreateReviewDto reviewDto)
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( userId == null )
                    return Unauthorized();

                await _reviews.UpdateReviewAsync(reviewId, reviewDto, userId );
                return NoContent();

            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpDelete( "{reviewId}" )]
        public async Task<IActionResult> DeleteReview ( int reviewId )
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( userId == null )
                    return Unauthorized();

                await _reviews.DeleteReviewAsync( reviewId, userId );
                return NoContent();
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );

            }
        }

    }
}
