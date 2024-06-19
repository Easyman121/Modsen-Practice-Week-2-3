#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class Orders
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public Users UserId { get; set; }
    public HashSet<OrderItems> OrderItems { get; set; } = [];
}