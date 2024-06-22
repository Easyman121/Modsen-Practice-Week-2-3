using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class AppContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var categories = modelBuilder.Entity<Categories>();
        categories.Property(t => t.Name).HasMaxLength(32);
        categories.Ignore(t => t.Products);
        categories.HasIndex(t => t.Name).IsUnique();

        var products = modelBuilder.Entity<Products>();
        products.Property(t => t.Name).HasMaxLength(32);
        products.Property(t => t.Description).HasMaxLength(256);
        products.HasIndex(t => t.Name).IsUnique();

        var orders = modelBuilder.Entity<Orders>();
        orders.Ignore(t => t.OrderItems);

        //var orderItems = modelBuilder.Entity<OrderItems>();

        var users = modelBuilder.Entity<Users>();
        users.Property(t => t.Email).HasMaxLength(32);
        users.Property(t => t.UserName).HasMaxLength(16);
        users.Ignore(t => t.Orders);
        users.HasIndex(t => t.Email).IsUnique();
        users.HasIndex(t => t.UserName).IsUnique();
    }
}