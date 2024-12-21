using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column( TypeName = "decimal(18,2)" )]
        public decimal TotalAmount { get; set; }

        [StringLength( 50 )]
        public string OrderStatus { get; set; } = "Pending";

        [StringLength( 200 )]
        public string? ShippingAddress { get; set; }

        [StringLength( 50 )]
        public string? TrackingNumber { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
