using BusinessLogicLayer.Exceptions;

namespace BusinessLogicLayer.Services;

public class ServiceHelper
{
    public static async Task<TEntity> CheckAndGetAsync<TEntity>
        (Func<int, CancellationToken, Task<TEntity>> getByIdAsync, int id, CancellationToken cancellationToken)
        where TEntity : DataAccessLayer.Models.Model
    {
        RequestDtoException.ThrowIfLessThan(id, 0);

        var data = await getByIdAsync(id, cancellationToken);
        if (data == null)
        {
            throw new RequestDtoException("No entries found");
        }

        return data;
    }

    public static async Task<List<TEntity>> CheckAndGetsAsync<TEntity>
        (Func<CancellationToken, Task<List<TEntity>>> getAllAsync, CancellationToken cancellationToken)
    {
        var prods = await getAllAsync(cancellationToken);

        if (prods.Count == 0)
        {
            throw new RequestDtoException("The list is empty");
        }

        return prods;
    }
}