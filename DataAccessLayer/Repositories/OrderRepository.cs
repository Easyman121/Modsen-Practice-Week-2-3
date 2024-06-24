using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

internal class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetOrdersByUserAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Order>()
            .Where(o => o.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken)
    {
        return await Context.Set<Order>()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductId)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }
}