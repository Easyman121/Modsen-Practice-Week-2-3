using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderItemController(ILogger<OrderItemController> logger) : ControllerBase
{
    [HttpGet(Name = "GetOrderItem")]
    public async Task<OrderItemResponseDto> GetOrderItem(int id, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpGet(Name = "GetOrderItems")]
    public async Task<IEnumerable<OrderItemResponseDto>> GetOrderItems(CancellationToken token) =>
        throw new NotImplementedException();

    [HttpPost(Name = "InsertOrderItem")]
    public async Task<int> Insert(OrderItemRequestDto orderItemDto, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpPost(Name = "UpdateOrderItem")]
    public async Task Update(int id, OrderItemRequestDto orderItemDto, CancellationToken token)
    {
        throw new NotImplementedException();
        return;
    }

    [HttpDelete(Name = "DeleteOrderItem")]
    public async Task Delete(int id, CancellationToken token) => throw new NotImplementedException();
}