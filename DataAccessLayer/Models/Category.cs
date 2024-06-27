// Using Fluent Api, not relevant
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class Category : Model
{
    public string Name { get; set; }

    public List<Product> Products { get; private set; } = [];
}