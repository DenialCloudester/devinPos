using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POSApp.Models;

namespace POSApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.HasIndex(e => e.SKU).IsUnique();
            });

            builder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
                entity.Property(e => e.TaxAmount).HasPrecision(18, 2);
                entity.Property(e => e.SubTotal).HasPrecision(18, 2);
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<SaleItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
                entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
                
                entity.HasOne(e => e.Sale)
                    .WithMany(s => s.SaleItems)
                    .HasForeignKey(e => e.SaleId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Coffee",
                    Description = "Premium coffee blend",
                    Price = 4.99m,
                    StockQuantity = 100,
                    Category = "Beverages",
                    SKU = "COF001",
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Sandwich",
                    Description = "Ham and cheese sandwich",
                    Price = 8.99m,
                    StockQuantity = 50,
                    Category = "Food",
                    SKU = "SAN001",
                    IsActive = true
                },
                new Product
                {
                    Id = 3,
                    Name = "Soda",
                    Description = "Refreshing cola drink",
                    Price = 2.49m,
                    StockQuantity = 200,
                    Category = "Beverages",
                    SKU = "SOD001",
                    IsActive = true
                }
            );
        }
    }
}
