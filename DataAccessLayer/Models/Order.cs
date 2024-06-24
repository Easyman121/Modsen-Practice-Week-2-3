#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
}