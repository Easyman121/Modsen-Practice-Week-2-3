#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace BusinessLogicLayer.DTO.Request;

public class OrderItemRequestDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
}