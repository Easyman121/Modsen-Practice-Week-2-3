using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

internal class ProductRepository : GenericRepository<Products>, IProductRepository
{
    public ProductRepository(AppContext context) : base(context)
    {
    }

    public async Task<List<Products>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await Context.Set<Products>()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }
}