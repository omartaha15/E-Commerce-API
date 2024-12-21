using E_Commerce_API.DTOs.UserDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterDto userRegisterDto);
        Task<string> Login(UserLoginDto userLoginDto);
        Task ResetPassword(PasswordResetDto passwordResetDto);
    }
}
