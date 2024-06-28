using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class OrderService
{
    private IUnitOfWork DataBase { get; set; }
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task<int> InsertOrderAsync(OrderRequestDto orderDto, CancellationToken cancellationToken)
    {
        CheckFields(orderDto, cancellationToken);
        var order = _mapper.Map<Order>(orderDto);
        return await DataBase.Order.InsertAsync(order, cancellationToken);
    }

    public async Task DeleteOrderAsync(int id, CancellationToken cancellationToken)
    {
        var order = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Order.GetByIdAsync, id, cancellationToken);
        await DataBase.Order.DeleteAsync(order, cancellationToken);
    }

    public async Task<OrderResponseDto> GetOrderAsync(int id, CancellationToken cancellationToken)
    {
        var order = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Order.GetOrderDetailsAsync, id,
            cancellationToken);
        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        var orders = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.Order.GetAllAsync, cancellationToken);

        return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }

    public async Task<UserResponseDto> GetUserAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Order.GetOrderDetailsAsync, orderId,
            cancellationToken);
        return _mapper.Map<UserResponseDto>(order.User);
    }

    private static void CheckFields(OrderRequestDto orderDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(orderDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfLessThan(orderDto.UserId, 0);
    }
}