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
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context; 
        }

        public virtual async Task<TEntity> GetById(int id) => await context.Set<TEntity>().FindAsync(id).AsTask();

        public virtual async Task<List<TEntity>> GetAll() => await context.Set<TEntity>().ToListAsync();

        public virtual async Task Insert(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        
        public virtual async Task Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
