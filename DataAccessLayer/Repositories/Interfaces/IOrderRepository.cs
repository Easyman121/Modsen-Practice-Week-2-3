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
        Task<List<Orders>> GetOrdersByUserAsync(int userId, CancellationToken cancellationToken);
        Task<Orders> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken); 
    }
}

