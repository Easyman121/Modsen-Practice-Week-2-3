using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions options) : base(options)
    {
        _ = Category!.Include(t => t.Products).ToList();
        _ = Order!.Include(t => t.OrderItems).ToList();
        _ = User!.Include(t => t.Orders);
    }

    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<User> User { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var category = modelBuilder.Entity<Category>();
        category.Property(t => t.Name).HasMaxLength(32);
        category.HasIndex(t => t.Name).IsUnique();

        var product = modelBuilder.Entity<Product>();
        product.Property(t => t.Name).HasMaxLength(32);
        product.Property(t => t.Description).HasMaxLength(256);
        product.HasIndex(t => t.Name).IsUnique();

        var user = modelBuilder.Entity<User>();
        user.Property(t => t.Email).HasMaxLength(32);
        user.Property(t => t.UserName).HasMaxLength(16);
        user.Property(t => t.PasswordHash).HasMaxLength(32);
        user.HasIndex(t => t.Email).IsUnique();
        user.HasIndex(t => t.UserName).IsUnique();
    }
}