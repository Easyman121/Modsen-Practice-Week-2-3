﻿using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services;

public class ServiceHelper
{
    public static async Task<TEntity> CheckAndGetEntityAsync<TEntity>
        (Func<int, CancellationToken, Task<TEntity?>> getByIdAsync, int id, CancellationToken cancellationToken)
        where TEntity : Model
    {
        RequestDtoException.ThrowIfLessThan(id, 0);

        var data = await getByIdAsync(id, cancellationToken);
        if (data == null)
        {
            throw new RequestDtoException("No entries found");
        }

        return data;
    }

    public static async Task<List<TEntity>> GetEntitiesAsync<TEntity>
        (Func<CancellationToken, Task<List<TEntity>>> getAllAsync, CancellationToken cancellationToken)
        where TEntity : Model =>
        await getAllAsync(cancellationToken);

    public static async Task<List<TEntity>> CheckAndGetEntitiesAsync<TEntity>
        (Func<CancellationToken, Task<List<TEntity>>> getAllAsync, CancellationToken cancellationToken)
        where TEntity : Model
    {
        var data = await GetEntitiesAsync<TEntity>(getAllAsync, cancellationToken);
        if (data == null)
        {
            throw new RequestDtoException("The list is empty");
        }

        return data;
    }

    public static async Task<List<TEntity>> CheckAndGetEntitiesByIdAsync<TEntity>
    (Func<int, CancellationToken, Task<List<TEntity>>> getAllByIdAsync, int id,
        CancellationToken cancellationToken)
        where TEntity : Model
    {
        var data = await getAllByIdAsync(id, cancellationToken);
        if (data.Count == 0)
        {
            throw new RequestDtoException("The list is empty");
        }

        return data;
    }
}