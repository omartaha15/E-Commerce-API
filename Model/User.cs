using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Model
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength( 100 )]
        public string FirstName { get; set; }

        [Required]
        [StringLength( 100 )]
        public string LastName { get; set; }

        [StringLength( 200 )]
        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime AccountCreated { get; set; } = DateTime.UtcNow;

        
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
