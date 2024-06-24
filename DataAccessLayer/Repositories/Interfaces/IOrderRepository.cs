using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetOrdersByUserAsync(int userId, CancellationToken cancellationToken);
    Task<Order?> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken);
}