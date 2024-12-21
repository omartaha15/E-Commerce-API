namespace E_Commerce_API.DTOs.ReviewDTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
