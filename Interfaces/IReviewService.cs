using E_Commerce_API.DTOs.ReviewDTOs;
using E_Commerce_API.Model;

namespace E_Commerce_API.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync ( int productId, CreateReviewDto createReviewDto, string userId );
        Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync ( int productId );
        Task UpdateReviewAsync ( int reviewId, CreateReviewDto updateReviewDto, string userId );
        Task DeleteReviewAsync ( int reviewId, string userId );
    }
}
