namespace DataAccessLayer.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken);
    void UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    void DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}