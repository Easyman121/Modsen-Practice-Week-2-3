// Using Fluent Api, not relevant
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class Products
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }
}