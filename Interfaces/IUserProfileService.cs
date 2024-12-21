using E_Commerce_API.DTOs.UserProfileDTOs;

namespace E_Commerce_API.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfileAsync ( string userId );
        Task UpdateUserProfileAsync ( string userId, UpdateUserProfileDto profileDto );

    }
}
