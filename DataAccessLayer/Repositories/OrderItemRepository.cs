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
    public class OrderItemRepository : GenericRepository<OrderItems>, IOrderItemRepository
    {
        public OrderItemRepository(AppContext context) : base(context)
        {
        }
    }
}
