using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs.UserProfileDTOs;
using E_Commerce_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_Commerce_API.Services
{
    public class UserProfileService : IUserProfileService 
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserProfileService ( ApplicationDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> GetUserProfileAsync ( string userId )
        {
            var user = await _context.Users.FirstOrDefaultAsync( u => u.Id == userId );
            if ( user == null )
                throw new KeyNotFoundException( "User not found." );

            return _mapper.Map<UserProfileDto>( user );
        }

        public async Task UpdateUserProfileAsync ( string userId, UpdateUserProfileDto profileDto )
        {
            var user = await _context.Users.FirstOrDefaultAsync( u => u.Id == userId );
            if ( user == null )
                throw new KeyNotFoundException( "User not found." );

            _mapper.Map( profileDto, user );
            _context.Users.Update( user );
            await _context.SaveChangesAsync();
        }
    }
}
