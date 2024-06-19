#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class Categories
{
    public int Id { get; set; }
    public string Name { get; set; }
    public HashSet<Products> Products { get; set; } = [];
}