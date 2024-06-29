using AutoMapper;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class OrderItemService(IUnitOfWork uow, IMapper mapper) : IOrderItemService
{
    public async Task<int> InsertOrderItemAsync(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(orderItemDto, cancellationToken);
        var allOrderItems = await ServiceHelper.GetEntitiesAsync(uow.OrderItem.GetAllAsync, cancellationToken);
        if (allOrderItems.Count > 0)
        {
            NonUniqueException.EnsureUnique(allOrderItems,
                c => c.OrderId == orderItemDto.OrderId && c.ProductId == orderItemDto.ProductId,
                "Order item is not unique");
        }

        var orderItem = mapper.Map<OrderItem>(orderItemDto);
        return await uow.OrderItem.InsertAsync(orderItem, cancellationToken);
    }

    public async Task UpdateOrderItemAsync(int id, OrderItemRequestDto orderItemDto,
        CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(orderItemDto, cancellationToken);
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, id, cancellationToken);
        var allOrderItems = await ServiceHelper.GetEntitiesAsync(uow.OrderItem.GetAllAsync, cancellationToken);
        if (orderItem.OrderId == orderItemDto.OrderId && orderItem.ProductId == orderItemDto.ProductId)
        {
            NonUniqueException.EnsureUnique(allOrderItems,
                c => c.OrderId == orderItemDto.OrderId && c.ProductId == orderItemDto.ProductId,
                "Order item is not unique");
        }

        orderItem.OrderId = orderItemDto.OrderId;
        orderItem.ProductId = orderItemDto.ProductId;
        orderItem.Count = orderItemDto.Count;
        await uow.OrderItem.UpdateAsync(orderItem, cancellationToken);
    }


    public async Task DeleteOrderItemAsync(int id, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, id, cancellationToken);
        await uow.OrderItem.DeleteAsync(orderItem, cancellationToken);
    }

    public async Task<OrderItemResponseDto> GetOrderItemAsync(int id, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, id, cancellationToken);
        return mapper.Map<OrderItemResponseDto>(orderItem);
    }

    public async Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsAsync(CancellationToken cancellationToken)
    {
        var orderItems =
            await ServiceHelper.GetEntitiesAsync(uow.OrderItem.GetAllAsync, cancellationToken);
        return mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItems);
    }

    public async Task<OrderResponseDto> GetOrderAsync(int orderItemId, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, orderItemId, cancellationToken);
        return mapper.Map<OrderResponseDto>(orderItem.Order);
    }

    public async Task<ProductResponseDto> GetProductAsync(int orderItemId, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, orderItemId, cancellationToken);
        return mapper.Map<ProductResponseDto>(orderItem.Product);
    }

    private static void CheckFieldsAndToken(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(orderItemDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfLessThan(orderItemDto.OrderId, 1);
        RequestDtoException.ThrowIfLessThan(orderItemDto.ProductId, 1);
    }
}