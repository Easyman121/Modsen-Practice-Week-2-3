#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace BusinessLogicLayer.DTO.Response;

public class OrderItemResponseDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public ProductResponseDto Product { get; set; }
    public int Count { get; set; }
}