using Application.Interfaces;
using Domain.Entity;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly WebSkladDbContext _context;
        private IRepository<Product> _productRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Outcome> _outcomeRepository;

        public UnitOfWork(WebSkladDbContext context)
        {
            _context = context;
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
        }

        public IRepository<Outcome> OutcomeRepository
        {
            get
            {
                if (_outcomeRepository == null)
                    _outcomeRepository = new OutcomeRepository(_context);
                return _outcomeRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
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
