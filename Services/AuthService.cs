using AutoMapper;
using E_Commerce_API.DTOs.UserDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService ( UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<string> Login ( UserLoginDto userLoginDto )
        {
            var user = await _userManager.FindByEmailAsync( userLoginDto.Email );

            if ( user == null )
                throw new Exception( "Invalid credentials" );

            var result = await _signInManager.CheckPasswordSignInAsync( user, userLoginDto.Password, false );

            if ( !result.Succeeded )
                throw new Exception( "Invalid credentials" );
            var Token = await GenerateJwtToken( user );
            return Token;
        }

        public async Task<string> Register ( UserRegisterDto userRegisterDto )
        {
            var user = _mapper.Map<User>(userRegisterDto);

            var result = await _userManager.CreateAsync( user , userRegisterDto.Password );

            if ( !result.Succeeded )
            {
                throw new Exception( string.Join( ", ", result.Errors.Select( e => e.Description ) ) );
            }


            var roleResult = await _userManager.AddToRoleAsync( user, "Customer" );
            
            if ( !roleResult.Succeeded )
            {
                throw new Exception( string.Join( ", ", roleResult.Errors.Select( e => e.Description ) ) );
            }

            return "User registered successfully with role 'Customer'";



        }

        public async Task ResetPassword ( PasswordResetDto passwordResetDto )
        {
            var user = await _userManager.FindByEmailAsync( passwordResetDto.Email );

            if ( user == null )
                throw new Exception( "User not found" );

            var result = await _userManager.ResetPasswordAsync( user, passwordResetDto.Token, passwordResetDto.NewPassword );

            if ( !result.Succeeded )
                throw new Exception( string.Join( ", ", result.Errors.Select( e => e.Description ) ) );

        }

        private async Task<string> GenerateJwtToken ( User user )
        {
            var roles = await _userManager.GetRolesAsync( user );

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            claims.AddRange( roles.Select( role => new Claim( ClaimTypes.Role, role ) ) );


            var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _configuration [ "JwtSettings:Key" ] ) );
            var creds = new SigningCredentials( key, SecurityAlgorithms.HmacSha256 );

            var token = new JwtSecurityToken(
               issuer: _configuration [ "JwtSettings:Issuer" ],
               audience: _configuration [ "JwtSettings:Audience" ],
               claims: claims,
               expires: DateTime.UtcNow.AddDays( 1 ),
               signingCredentials: creds );

            return new JwtSecurityTokenHandler().WriteToken( token );
        }
    }
}
