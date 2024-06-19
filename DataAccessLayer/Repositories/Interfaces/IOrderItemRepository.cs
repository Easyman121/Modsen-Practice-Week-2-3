using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItems>
    {
        Task<OrderItems> GetOrderItemById(int id);

    }
}
