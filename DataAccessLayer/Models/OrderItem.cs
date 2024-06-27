#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class OrderItem : Model
{
    public int Count { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}