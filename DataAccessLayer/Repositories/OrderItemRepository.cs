using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppContext context) : base(context)
    {
    }
}