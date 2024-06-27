#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace BusinessLogicLayer.DTO.Request;

public class OrderRequestDto
{
    public DateTime DateTime { get; set; }
    public int UserId { get; set; }
}