using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IOrderService
{
    Task<int> InsertOrderAsync(OrderRequestDto orderDto, CancellationToken cancellationToken);
    Task DeleteOrderAsync(int id, OrderRequestDto orderDto, CancellationToken cancellationToken);
    Task<OrderResponseDto> GetOrderAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(CancellationToken cancellationToken);
    Task<UserResponseDto> GetUserAsync(int orderId, CancellationToken cancellationToken);
}