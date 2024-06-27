// Using Fluent Api, not relevant
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DataAccessLayer.Models;

public class User : Model
{
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public string Email { get; set; }

    public List<Order> Orders { get; private set; } = [];
}