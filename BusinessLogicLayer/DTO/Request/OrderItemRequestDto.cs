#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace BusinessLogicLayer.DTO.Request;

public class OrderItemRequestDto
{
    public ProductRequestDto Product { get; set; }
    public int Count { get; set; }
}