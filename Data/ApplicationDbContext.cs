using E_Commerce_API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace E_Commerce_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }


        
        public DbSet<User> ApplicationUsers { get; set; }

        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating ( ModelBuilder builder )
        {
            base.OnModelCreating( builder );

            // Fluent API configurations
            builder.Entity<Product>()
                .HasIndex( p => p.Name );

            builder.Entity<Order>()
                .HasIndex( o => o.OrderDate );

            builder.Entity<Review>()
                .HasOne( r => r.User )
                .WithMany( u => u.Reviews )
                .HasForeignKey( r => r.UserId )
                .OnDelete( DeleteBehavior.Restrict );

            builder.Entity<Order>()
                .HasOne( o => o.User )
                .WithMany( u => u.Orders )
                .HasForeignKey( o => o.UserId )
                .OnDelete( DeleteBehavior.Restrict );


            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "3", Name = "Manager", NormalizedName = "MANAGER" }
            );

            // Seed some initial data 
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Books" }
            );
        }
    }
}
