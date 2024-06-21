using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IOrderItem
{
    Task SetOrderItemAsync(OrderItemRequestDto orderItemDto);
    Task<OrderItemResponseDto> GetOrderItemAsync(int id);
    Task<IEnumerable<OrderItemResponseDto>> GetOrderItemAsync();
}