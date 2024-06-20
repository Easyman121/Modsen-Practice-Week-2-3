using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    //The UnitOfWork class provides access to repositories
    //and provides a common context for all repositories.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext _context;

        public ICategoryRepository Category { get; }
        public IOrderItemRepository OrderItem { get; }
        public IOrderRepository Order { get; }
        public IUserRepository User { get; }
        public IProductRepository Product { get; }

        public UnitOfWork(AppContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            OrderItem = new OrderItemRepository(_context);
            Order = new OrderRepository(_context);
            User = new UserRepository(_context);
            Product = new ProductRepository(_context);
        }

        public async Task SaveAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

        private bool disposed = false;

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public async void DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}
