using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork
    {
        private readonly DbContext _context;

        public ICategoryRepository Category { get; }
        public IOrderItemRepository OrderItem { get; }
        public IOrderRepository Order { get; }
        public IUserRepository User { get; }
        public IProductRepository Product { get; }

        public UnitOfWork(DbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            OrderItem = new OrderItemRepository(_context);
            Order = new OrderRepository(_context);
            User = new UserRepository(_context);
            Product = new ProductRepository(_context);
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
