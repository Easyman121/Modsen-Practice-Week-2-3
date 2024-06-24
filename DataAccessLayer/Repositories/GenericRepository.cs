using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

//Generic repository covers CRUD operations
public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected AppContext Context;

    public GenericRepository(AppContext context)
    {
        Context = context;
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await Context.Set<TEntity>().FindAsync([id], cancellationToken).AsTask();

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await Context.Set<TEntity>().ToListAsync(cancellationToken);

    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async void UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async void DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}