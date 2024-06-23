using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class CategoryRepository : GenericRepository<Categories>, ICategoryRepository
{
    public CategoryRepository(AppContext context) : base(context)
    {
    }
}