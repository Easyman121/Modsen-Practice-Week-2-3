using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await Context.Set<Product>()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }
}