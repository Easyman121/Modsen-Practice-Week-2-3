using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

//Generic repository covers CRUD operations
public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Model
{
    protected AppContext Context;

    public GenericRepository(AppContext context)
    {
        Context = context;
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await Context.Set<TEntity>().FindAsync([id], cancellationToken).AsTask();

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await Context.Set<TEntity>().OrderBy(t => t.Id).ToListAsync(cancellationToken);

    public virtual async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var entityEntry = await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entityEntry.Entity.Id;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}