#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace BusinessLogicLayer.DTO.Request;

public class UserRequestDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}