using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Orders>
{
    Task<List<Orders>> GetOrdersByUserAsync(int userId, CancellationToken cancellationToken);
    Task<Orders?> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken);
}