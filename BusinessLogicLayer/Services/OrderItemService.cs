using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class OrderItemService(IUnitOfWork DataBase) : IOrderItemService
{
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task<int> InsertOrderItemAsync(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken)
    {
        CheckFields(orderItemDto, cancellationToken);
        var allOrderItems = await ServiceHelper.GetEntitiesAsync(DataBase.OrderItem.GetAllAsync, cancellationToken);
        NonUniqueException.EnsureUnique(allOrderItems,
            c => c.OrderId == orderItemDto.OrderId && c.ProductId == orderItemDto.ProductId,
            "Same order item requested");
        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        return await DataBase.OrderItem.InsertAsync(orderItem, cancellationToken);
    }

    public async Task UpdateOrderItemAsync(int id, OrderItemRequestDto orderItemDto,
        CancellationToken cancellationToken)
    {
        CheckFields(orderItemDto, cancellationToken);
        var oldOrderItem =
            await ServiceHelper.CheckAndGetEntityAsync(DataBase.OrderItem.GetByIdAsync, id, cancellationToken);
        var allOrderItems = await ServiceHelper.GetEntitiesAsync(DataBase.OrderItem.GetAllAsync, cancellationToken);
        if (oldOrderItem.OrderId == orderItemDto.OrderId && oldOrderItem.ProductId == orderItemDto.ProductId)
        {
            NonUniqueException.EnsureUnique(allOrderItems,
                c => c.OrderId == orderItemDto.OrderId && c.ProductId == orderItemDto.ProductId,
                "Same order item requested");
        }

        var newOrderItem = _mapper.Map<OrderItem>(orderItemDto);
        newOrderItem.Id = oldOrderItem.Id;
        await DataBase.OrderItem.UpdateAsync(newOrderItem, cancellationToken);
    }


    public async Task DeleteOrderItemAsync(int id, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(DataBase.OrderItem.GetByIdAsync, id, cancellationToken);
        await DataBase.OrderItem.DeleteAsync(orderItem, cancellationToken);
    }

    public async Task<OrderItemResponseDto> GetOrderItemAsync(int id, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(DataBase.OrderItem.GetByIdAsync, id, cancellationToken);
        return _mapper.Map<OrderItemResponseDto>(orderItem);
    }

    public async Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsAsync(CancellationToken cancellationToken)
    {
        var orderItems =
            await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.OrderItem.GetAllAsync, cancellationToken);
        return _mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItems);
    }

    public async Task<OrderResponseDto> GetOrderAsync(int orderItemId, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(DataBase.OrderItem.GetByIdAsync, orderItemId, cancellationToken);
        return _mapper.Map<OrderResponseDto>(orderItem.Order);
    }

    public async Task<ProductResponseDto> GetProductAsync(int orderItemId, CancellationToken cancellationToken)
    {
        var orderItem =
            await ServiceHelper.CheckAndGetEntityAsync(DataBase.OrderItem.GetByIdAsync, orderItemId, cancellationToken);
        return _mapper.Map<ProductResponseDto>(orderItem.Product);
    }

    private static void CheckFields(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(orderItemDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfLessThan(orderItemDto.OrderId, 0);
        RequestDtoException.ThrowIfLessThan(orderItemDto.ProductId, 0);
    }
}