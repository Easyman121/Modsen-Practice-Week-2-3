using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IOrderService
{
    Task InsertOrderAsync(OrderRequestDto orderDto, CancellationToken cancellationToken);
    Task DeleteOrderAsync(int id, OrderRequestDto orderDto, CancellationToken cancellationToken);
    Task<OrderResponseDto> GetOrderAsync(int id, CancellationToken cancellationToken);
    Task<UserResponseDto> GetUserAsync(OrderRequestDto orderDto, CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(CancellationToken cancellationToken);
}