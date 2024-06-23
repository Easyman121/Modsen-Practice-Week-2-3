using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IProductRepository : IRepository<Products>
{
    Task<List<Products>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken);
}