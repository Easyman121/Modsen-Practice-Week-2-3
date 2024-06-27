#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace BusinessLogicLayer.DTO.Response;

public class OrderResponseDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int UserId { get; set; }
    public List<OrderItemResponseDto> Items { get; set; }
}