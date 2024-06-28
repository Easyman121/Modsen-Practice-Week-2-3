using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderItemController(ILogger<OrderItemController> logger, IOrderItemService orderItemService)
    : ControllerBase
{
    [HttpGet(Name = "GetOrderItem")]
    public async Task<OrderItemResponseDto> GetOrderItemAsync(int id, CancellationToken token) =>
        await orderItemService.GetOrderItemAsync(id, token);

    [HttpGet(Name = "GetOrderItems")]
    public async Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsAsync(CancellationToken token) =>
        await orderItemService.GetOrderItemsAsync(token);

    [HttpGet(Name = "GetOrder")]
    public async Task<OrderResponseDto> GetOrderAsync(int id, CancellationToken token) =>
        await orderItemService.GetOrderAsync(id, token);

    [HttpGet(Name = "GetProduct")]
    public async Task<ProductResponseDto> GetProductAsync(int id, CancellationToken token) =>
        await orderItemService.GetProductAsync(id, token);

    [HttpPost(Name = "InsertOrderItem")]
    public async Task<int> InsertAsync(OrderItemRequestDto orderItemDto, CancellationToken token) =>
        await orderItemService.InsertOrderItemAsync(orderItemDto, token);

    [HttpPost(Name = "UpdateOrderItem")]
    public async Task UpdateAsync(int id, OrderItemRequestDto orderItemDto, CancellationToken token) =>
        await orderItemService.UpdateOrderItemAsync(id, orderItemDto, token);

    [HttpDelete(Name = "DeleteOrderItem")]
    public async Task DeleteAsync(int id, CancellationToken token) =>
        await orderItemService.DeleteOrderItemAsync(id, token);
}