using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    internal class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        public ProductRepository(AppContext context) : base(context)
        {
        }
        public async Task<List<Products>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await context.Set<Products>()
                .Where(p => p.CategoryId.Id == categoryId)
                .ToListAsync(cancellationToken);
        }

    }
}
