using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

internal class OrderRepository : GenericRepository<Orders>, IOrderRepository
{
    public OrderRepository(AppContext context) : base(context)
    {
    }

    public async Task<List<Orders>> GetOrdersByUserAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Orders>()
            .Where(o => o.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Orders?> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken)
    {
        return await Context.Set<Orders>()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductId)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }
}