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
    public class CategoryRepository : GenericRepository<Categories>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }
        public async Task<List<Categories>> GetAllCategories()
        {
            return await context.Set<Categories>()
                .ToListAsync();
        }

        public async Task<Categories> GetCategoryById(int categoryId)
        {
            return await context.Set<Categories>()
                .FindAsync(categoryId);
        }
    }
}
