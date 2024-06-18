#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class Users
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public int PasswordHash { get; set; }
    public string Email { get; set; }
}