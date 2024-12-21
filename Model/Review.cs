using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Range( 1, 5 )]
        public int Rating { get; set; }

        [StringLength( 1000 )]
        public string? Comment { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}