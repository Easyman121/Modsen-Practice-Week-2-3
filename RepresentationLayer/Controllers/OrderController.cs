using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController(ILogger<OrderController> logger) : ControllerBase
{
    [HttpGet(Name = "GetOrder")]
    public async Task<OrderResponseDto> GetOrder(int id, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpGet(Name = "GetOrders")]
    public async Task<IEnumerable<OrderResponseDto>> GetOrders(CancellationToken token) =>
        throw new NotImplementedException();

    [HttpPost(Name = "InsertOrder")]
    public async Task<int> Insert(OrderRequestDto orderDto, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpPost(Name = "UpdateOrder")]
    public async Task Update(int id, OrderRequestDto orderDto, CancellationToken token)
    {
        throw new NotImplementedException();
        return;
    }

    [HttpDelete(Name = "DeleteOrder")]
    public async Task Delete(int id, CancellationToken token) => throw new NotImplementedException();
}