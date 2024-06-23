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
    internal class OrderRepository: GenericRepository<Orders>, IOrderRepository
    {
        public OrderRepository(AppContext context) : base(context)
        {
        }

        public async Task<List<Orders>> GetOrdersByUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await context.Set<Orders>()
                .Where(o => o.UserId.Id == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Orders> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken)
        {
            return await context.Set<Orders>()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductId)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }
    }
}
