using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IOrderItemService
{
    Task<int> InsertOrderItemAsync(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken);
    Task UpdateOrderItemAsync(int id, CancellationToken cancellationToken);
    Task DeleteOrderItemAsync(int id, CancellationToken cancellationToken);
    Task<OrderItemResponseDto> GetOrderItemAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsAsync(CancellationToken cancellationToken);
    Task<OrderResponseDto> GetOrderAsync(int orderItemId, CancellationToken cancellationToken);
    Task<ProductResponseDto> GetProductAsync(int orderItemId, CancellationToken cancellationToken);
}