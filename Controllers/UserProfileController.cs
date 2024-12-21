using E_Commerce_API.DTOs.UserProfileDTOs;
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
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userService;

        public UserProfileController ( IUserProfileService userService )
        {
            _userService = userService;
        }

       


        [HttpGet( "profile" )]
        public async Task<IActionResult> GetProfile ()
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( string.IsNullOrEmpty( userId ) )
                    return Unauthorized( new { message = "User not authenticated" } );
                var profile = await _userService.GetUserProfileAsync( userId );
                return Ok( profile );
            }
            catch ( KeyNotFoundException ex )
            {
                return NotFound( new { Message = ex.Message } );
            }
            catch ( Exception ex )
            {
                return StatusCode( 500, new { Message = ex.Message } );
            }
        }


        [HttpPut( "profile" )]
        public async Task<IActionResult> UpdateProfile ( [FromBody] UpdateUserProfileDto profileDto )
        {
            try
            {
                var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
                if ( string.IsNullOrEmpty( userId ) )
                    return Unauthorized( new { message = "User not authenticated" } );
                await _userService.UpdateUserProfileAsync( userId, profileDto );
                return NoContent();
            }
            catch ( KeyNotFoundException ex )
            {
                return NotFound( new { Message = ex.Message } );
            }
            catch ( Exception ex )
            {
                return StatusCode( 500, new { Message = ex.Message } );
            }
        }
    }
}
