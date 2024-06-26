using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IOrderService
{
    Task SetOrderAsync(OrderRequestDto orderDto, CancellationToken cancellationToken);
    Task<OrderResponseDto> GetOrderAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(CancellationToken cancellationToken);
}