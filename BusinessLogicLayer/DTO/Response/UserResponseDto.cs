namespace BusinessLogicLayer.DTO.Response;

public class UserResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<OrderItemResponseDto> OrderItems { get; set; } = [];
}
