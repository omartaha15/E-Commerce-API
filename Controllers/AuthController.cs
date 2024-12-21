using E_Commerce_API.DTOs.UserDTOs;
using E_Commerce_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController ( IAuthService authService )
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register ( [FromBody] UserRegisterDto user )
        {
            try
            {
                var result = await _authService.Register( user );
                return Ok( new { message = result } );
            }
            catch ( Exception ex )
            {
                return BadRequest( new { error = ex.Message } );
            }
        }

        [HttpPost( "login" )]
        public async Task<ActionResult> Login ( [FromBody] UserLoginDto user )
        {
            try
            {
                var token = await _authService.Login( user );
                return Ok( new { token } );
            }
            catch ( Exception ex )
            {
                return Unauthorized( new { error = ex.Message } );
            }
        }

        

        [HttpPost( "reset-password" )]
        public async Task<IActionResult> ResetPassword ( PasswordResetDto dto )
        {
            try 
            { 
                await _authService.ResetPassword( dto );
                return Ok( new { message = "Password reset successfully" } );

            }
            catch ( Exception ex )
            {
                return BadRequest(new {error = ex.Message});
            }
        }

        
    }
}
