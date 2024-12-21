using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API.Model
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 200 )]
        public string Name { get; set; }

        [StringLength( 1000 )]
        public string? Description { get; set; }

        [Column( TypeName = "decimal(18,2)" )]
        public decimal Price { get; set; }

        [Column( TypeName = "decimal(18,2)" )]
        public decimal? DiscountPrice { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
