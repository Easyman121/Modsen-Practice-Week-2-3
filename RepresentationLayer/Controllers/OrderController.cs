using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController(ILogger<OrderController> logger, IOrderService orderService) : ControllerBase
{
    [HttpGet(Name = "GetOrder")]
    public async Task<OrderResponseDto> GetOrderAsync(int id, CancellationToken token) =>
        await orderService.GetOrderAsync(id, token);

    [HttpGet(Name = "GetOrders")]
    public async Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(CancellationToken token) =>
        await orderService.GetOrdersAsync(token);

    [HttpGet(Name = "GetUser")]
    public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken token) =>
        await orderService.GetUserAsync(id, token);

    [HttpPost(Name = "InsertOrder")]
    public async Task<int> InsertAsync(OrderRequestDto orderDto, CancellationToken token) =>
        await orderService.InsertOrderAsync(orderDto, token);

    [HttpDelete(Name = "DeleteOrder")]
    public async Task DeleteAsync(int id, CancellationToken token) =>
        await orderService.DeleteOrderAsync(id, token);
}