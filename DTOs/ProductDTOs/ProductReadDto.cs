using E_Commerce_API.DTOs.CategoryDTOs;

namespace E_Commerce_API.DTOs.ProductDTOs
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public CategoryDTO Category { get; set; }
        public List<ProductImageDto> Images { get; set; }
        public decimal? AverageRating { get; set; }
    }
}
