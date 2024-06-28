using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController(ILogger<UserController> logger) : ControllerBase
{
    [HttpGet(Name = "GetUser")]
    public async Task<UserResponseDto> GetUser(int id, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpGet(Name = "GetUsers")]
    public async Task<IEnumerable<UserResponseDto>> GetUsers(CancellationToken token) =>
        throw new NotImplementedException();

    [HttpGet(Name = "GetUserOrders")]
    public async Task<IEnumerable<OrderResponseDto>> GetOrders(int userId) => throw new NotImplementedException();

    [HttpPost(Name = "InsertUser")]
    public async Task<int> Insert(UserRequestDto userDto, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpPost(Name = "UpdateUser")]
    public async Task Update(int id, UserRequestDto userDto, CancellationToken token)
    {
        throw new NotImplementedException();
        return;
    }

    [HttpDelete(Name = "DeleteUser")]
    public async Task Delete(int id, CancellationToken token) => throw new NotImplementedException();
}