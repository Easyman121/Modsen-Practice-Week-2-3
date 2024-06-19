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
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
        public async Task<Users> GetUserById(int id)
        {
            return await context.Set<Users>().FindAsync(id);
        }
    }
}
