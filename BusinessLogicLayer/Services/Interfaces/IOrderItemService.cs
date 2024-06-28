using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IOrderItemService
{
    Task SetOrderItemAsync(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken);
    Task<OrderItemResponseDto> GetOrderItemAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderItemResponseDto>> GetOrderItemAsync(CancellationToken cancellationToken);
}