using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController(ILogger<UserController> logger, IUserService userService) : ControllerBase
{
    [HttpGet(Name = "GetUser")]
    public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken token) =>
        await userService.GetUserAsync(id, token);

    [HttpGet(Name = "GetUsers")]
    public async Task<IEnumerable<UserResponseDto>> GetUsersAsync(CancellationToken token) =>
        await userService.GetUsersAsync(token);

    [HttpGet(Name = "GetOrders")]
    public async Task<IEnumerable<OrderResponseDto>> GetOrders(int id, CancellationToken token) =>
        await userService.GetOrdersAsync(id, token);

    [HttpPost(Name = "InsertUser")]
    public async Task<int> Insert(UserRequestDto userDto, CancellationToken token) =>
        await userService.InsertUserAsync(userDto, token);

    [HttpPost(Name = "UpdateUser")]
    public async Task Update(int id, UserRequestDto userDto, CancellationToken token) =>
        await userService.UpdateUserAsync(id, userDto, token);

    [HttpDelete(Name = "DeleteUser")]
    public async Task Delete(int id, CancellationToken token) =>
        await userService.DeleteUserAsync(id, token);
}