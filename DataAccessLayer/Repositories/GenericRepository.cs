using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    //Generic repository covers CRUD operations
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected AppContext context;

        public GenericRepository(AppContext context)
        {
            this.context = context; 
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken) 
            => await context.Set<TEntity>().FindAsync([id], cancellationToken).AsTask();

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken) 
            => await context.Set<TEntity>().ToListAsync(cancellationToken);

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        
        public virtual void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }
    }
}
