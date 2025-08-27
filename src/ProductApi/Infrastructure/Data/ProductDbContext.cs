namespace ProductApi.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using ProductApi.Domain;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity properties and relationships if needed
        modelBuilder.Entity<Product>().ToTable("Products").HasData(
            new Product { ProductId = 123456, Name = "VisionPro Eyeglass Lens", Category = "Eyewear", Price = 149.99m, AvailableStock = 120 },
            new Product { ProductId = 123457, Name = "BlueLight Blocking Glasses", Category = "Eyewear", Price = 89.50m, AvailableStock = 200 },
            new Product { ProductId = 123458, Name = "Precision Microscope X200", Category = "Medical", Price = 5499.00m, AvailableStock = 15 },
            new Product { ProductId = 123459, Name = "Portable Diagnostic Kit", Category = "Medical", Price = 1299.00m, AvailableStock = 25 },
            new Product { ProductId = 123460, Name = "CinePrime 50mm Camera Lens", Category = "Electronics", Price = 899.50m, AvailableStock = 40 },
            new Product { ProductId = 123461, Name = "Compact Projector StarView", Category = "Electronics", Price = 799.00m, AvailableStock = 12 },
            new Product { ProductId = 123462, Name = "VR Headset VisionX", Category = "Electronics", Price = 499.00m, AvailableStock = 60 }
        );

        modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();
    }
}
