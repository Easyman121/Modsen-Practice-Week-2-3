using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class OrderService(IUnitOfWork uow) : IOrderService
{
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task<int> InsertOrderAsync(OrderRequestDto orderDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(orderDto, cancellationToken);
        var order = _mapper.Map<Order>(orderDto);
        order.User = await ServiceHelper.CheckAndGetEntityAsync(uow.User.GetByIdAsync, order.UserId, cancellationToken);
        return await uow.Order.InsertAsync(order, cancellationToken);
    }

    public async Task DeleteOrderAsync(int id, CancellationToken cancellationToken)
    {
        var order = await ServiceHelper.CheckAndGetEntityAsync(uow.Order.GetByIdAsync, id, cancellationToken);
        await uow.Order.DeleteAsync(order, cancellationToken);
    }

    public async Task<OrderResponseDto> GetOrderAsync(int id, CancellationToken cancellationToken)
    {
        var order = await ServiceHelper.CheckAndGetEntityAsync(uow.Order.GetOrderDetailsAsync, id,
            cancellationToken);
        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        var orders = await ServiceHelper.CheckAndGetEntitiesAsync(uow.Order.GetAllAsync, cancellationToken);

        return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }

    public async Task<UserResponseDto> GetUserAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await ServiceHelper.CheckAndGetEntityAsync(uow.Order.GetOrderDetailsAsync, orderId,
            cancellationToken);
        return _mapper.Map<UserResponseDto>(order.User);
    }

    private static void CheckFieldsAndToken(OrderRequestDto orderDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(orderDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfLessThan(orderDto.UserId, 1);
    }
}