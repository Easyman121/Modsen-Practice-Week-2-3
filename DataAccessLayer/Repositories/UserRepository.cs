using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppContext context) : base(context)
    {
    }
}