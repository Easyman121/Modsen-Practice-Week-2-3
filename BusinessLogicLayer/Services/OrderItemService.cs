using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class OrderItemService(IUnitOfWork uow) : IOrderItemService
{
    private readonly IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

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

        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        return await uow.OrderItem.InsertAsync(orderItem, cancellationToken);
    }

    public async Task UpdateOrderItemAsync(int id, OrderItemRequestDto orderItemDto,
        CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(orderItemDto, cancellationToken);
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, id, cancellationToken);
        var allOrderItems = await ServiceHelper.CheckAndGetEntitiesAsync(uow.OrderItem.GetAllAsync, cancellationToken);
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
        return _mapper.Map<OrderItemResponseDto>(orderItem);
    }

    public async Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsAsync(CancellationToken cancellationToken)
    {
        var orderItems =
            await ServiceHelper.CheckAndGetEntitiesAsync(uow.OrderItem.GetAllAsync, cancellationToken);
        return _mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItems);
    }

    public async Task<OrderResponseDto> GetOrderAsync(int orderItemId, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, orderItemId, cancellationToken);
        return _mapper.Map<OrderResponseDto>(orderItem.Order);
    }

    public async Task<ProductResponseDto> GetProductAsync(int orderItemId, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(uow.OrderItem.GetByIdAsync, orderItemId, cancellationToken);
        return _mapper.Map<ProductResponseDto>(orderItem.Product);
    }

    private static void CheckFieldsAndToken(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(orderItemDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfLessThan(orderItemDto.OrderId, 1);
        RequestDtoException.ThrowIfLessThan(orderItemDto.ProductId, 1);
    }
}