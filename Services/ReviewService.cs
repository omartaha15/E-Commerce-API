using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs.ReviewDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewService ( ApplicationDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddReviewAsync ( int productId, CreateReviewDto createReviewDto, string userId )
        {
            var review = _mapper.Map<Review>( createReviewDto );
            review.ProductId = productId;
            review.UserId = userId;
            review.ReviewDate = DateTime.Now;

            await _context.Reviews.AddAsync( review );
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync ( int reviewId, string userId )
        {
           var review = await _context.Reviews.FindAsync( reviewId );
            if( review == null)
            {
                throw new UnauthorizedAccessException( "Review not found." );
            }

            if( review.UserId != userId )
            {
                throw new UnauthorizedAccessException( "Access denied." );
            }

            _context.Reviews.Remove( review );
            await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync ( int productId )
        {
            var reviews = await _context.Reviews
                 .Where( r => r.ProductId == productId )
                 .Include( r => r.User )
                 .ToListAsync();

            return _mapper.Map<IEnumerable<ReviewDto>>( reviews );
        }

        public async Task UpdateReviewAsync ( int reviewId, CreateReviewDto updateReviewDto, string userId )
        {
            var review = await _context.Reviews.FindAsync ( reviewId );

            if ( review == null || review.UserId != userId )
            {
                throw new UnauthorizedAccessException( "Review not found or access denied." );
            }

            review.Rating = updateReviewDto.Rating;
            review.Comment = updateReviewDto.Comment;

            await _context.SaveChangesAsync ();
        }
    }
}
