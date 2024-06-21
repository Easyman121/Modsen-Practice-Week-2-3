using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IOrderService
{
    Task SetOrderAsync(OrderRequestDto orderDto);
    Task<OrderResponseDto> GetOrderAsync(int? id);
    Task<IEnumerable<OrderResponseDto>> GetOrdersAsync();
}