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
    public class OrderItemRepository : GenericRepository<OrderItems>, IOrderItemRepository
    {
        public OrderItemRepository(DbContext context) : base(context)
        {
        }
        public async Task<OrderItems> GetOrderItemById(int id)
        {
            return await context.Set<OrderItems>()
                .FindAsync(id);
        }
    }
}
