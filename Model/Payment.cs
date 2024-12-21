using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [StringLength( 50 )]
        public string PaymentMethod { get; set; }

        [Column( TypeName = "decimal(18,2)" )]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [StringLength( 50 )]
        public string PaymentStatus { get; set; } = "Pending";
    }
}