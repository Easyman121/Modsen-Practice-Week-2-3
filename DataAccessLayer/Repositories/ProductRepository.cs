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
        public ProductRepository(DbContext context) : base(context)
        {
        }
        public async Task<List<Products>> GetProductsByCategory(int categoryId)
        {
            return await context.Set<Products>()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Products> GetProductById(int productId)
        {
            return await context.Set<Products>()
                .FindAsync(productId);
        }
    }
}
