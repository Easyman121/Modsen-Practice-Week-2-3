﻿using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class OrderItemService(IUnitOfWork DataBase) : IOrderItemService
{
    public Task<int> InsertOrderItemAsync(OrderItemRequestDto orderItemDto, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task UpdateOrderItemAsync(int id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task DeleteOrderItemAsync(int id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<OrderItemResponseDto> GetOrderItemAsync(int id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsAsync(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<OrderResponseDto> GetOrderAsync(int orderItemId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<ProductResponseDto> GetProductAsync(int orderItemId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}