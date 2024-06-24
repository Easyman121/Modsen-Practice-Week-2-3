using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppContext context) : base(context)
    {
    }
}