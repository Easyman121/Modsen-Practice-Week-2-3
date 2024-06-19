using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetAll();
        Task Insert(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
    }
}
