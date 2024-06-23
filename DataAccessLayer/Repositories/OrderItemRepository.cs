using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class OrderItemRepository : GenericRepository<OrderItems>, IOrderItemRepository
{
    public OrderItemRepository(AppContext context) : base(context)
    {
    }
}