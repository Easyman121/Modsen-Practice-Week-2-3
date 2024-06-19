using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    internal class OrderRepository: GenericRepository<Orders>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
        public async Task<List<Orders>> GetAllOrders()
        {
            return await context.Set<Orders>()
                                .ToListAsync();
        }

        public async Task<List<Orders>> GetOrdersByUser(int userId)
        {
            return await context.Set<Orders>()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Orders> GetOrderById(int orderId)
        {
            return await context.Set<Orders>()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task CreateOrder(Orders order)
        {
            await context.Set<Orders>().AddAsync(order);
            await context.SaveChangesAsync();
        }
        public async Task<Orders> GetOrderDetails(int orderId)
        {
            return await context.Set<Orders>()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductId)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
