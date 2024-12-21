using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Model
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [StringLength( 500 )]
        public string ImageUrl { get; set; }

        public bool IsPrimaryImage { get; set; } = false;
    }
}