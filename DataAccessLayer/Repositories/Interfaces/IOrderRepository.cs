using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Orders>
    {
        Task<List<Orders>> GetAllOrders();
        Task<List<Orders>> GetOrdersByUser(int userId);
        Task<Orders> GetOrderById(int orderId);
        Task CreateOrder(Orders order);
        Task<Orders> GetOrderDetails(int orderId); 

    }
}

