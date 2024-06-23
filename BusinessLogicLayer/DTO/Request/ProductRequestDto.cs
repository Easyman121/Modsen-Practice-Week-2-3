#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace BusinessLogicLayer.DTO.Request;

public class ProductRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string CategoryName { get; set; }
}