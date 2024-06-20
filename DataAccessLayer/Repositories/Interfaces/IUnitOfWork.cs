using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public ICategoryRepository Category { get; }
        public IOrderItemRepository OrderItem { get; }
        public IOrderRepository Order { get; }
        public IUserRepository User { get; }
        public IProductRepository Product { get; }
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
