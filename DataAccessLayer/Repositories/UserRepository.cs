using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class UserRepository : GenericRepository<Users>, IUserRepository
{
    public UserRepository(AppContext context) : base(context)
    {
    }
}