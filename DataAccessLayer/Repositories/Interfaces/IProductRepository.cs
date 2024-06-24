using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken);
}