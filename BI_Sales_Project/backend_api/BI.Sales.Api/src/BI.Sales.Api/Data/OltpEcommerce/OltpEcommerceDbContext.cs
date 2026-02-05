using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Data.OltpEcommerce;

public class OltpEcommerceDbContext : IdentityDbContext<ApplicationUser>
{
    public OltpEcommerceDbContext(DbContextOptions<OltpEcommerceDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<SubCategory> SubCategories => Set<SubCategory>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        builder.Entity<Product>()
            .HasIndex(p => p.Sku)
            .IsUnique();

        builder.Entity<ShoppingCart>()
            .HasKey(c => c.CartId);

        builder.Entity<ShoppingCart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.Cart)
            .HasForeignKey(i => i.CartId);

        builder.Entity<Order>()
            .HasIndex(o => o.OrderNumber)
            .IsUnique();
    }
}
