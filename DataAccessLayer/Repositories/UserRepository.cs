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
        public UserRepository(AppContext context) : base(context)
        {
        }
    }
}
