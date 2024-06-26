using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Model
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}