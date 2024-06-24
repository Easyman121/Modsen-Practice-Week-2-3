using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class AppContext(DbContextOptions options) : DbContext(options)
{
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
        category.Ignore(t => t.Products);
        category.HasIndex(t => t.Name).IsUnique();

        var product = modelBuilder.Entity<Product>();
        product.Property(t => t.Name).HasMaxLength(32);
        product.Property(t => t.Description).HasMaxLength(256);
        product.HasIndex(t => t.Name).IsUnique();

        var order = modelBuilder.Entity<Order>();
        order.Ignore(t => t.OrderItems);

        //var orderItem = modelBuilder.Entity<OrderItem>();

        var user = modelBuilder.Entity<User>();
        user.Property(t => t.Email).HasMaxLength(32);
        user.Property(t => t.UserName).HasMaxLength(16);
        user.Ignore(t => t.Orders);
        user.HasIndex(t => t.Email).IsUnique();
        user.HasIndex(t => t.UserName).IsUnique();
    }
}