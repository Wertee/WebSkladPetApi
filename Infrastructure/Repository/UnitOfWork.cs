using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private WebSkladDbContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IOutcomeRepository _outcomeRepository;

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
        }

        public IOutcomeRepository OutcomeRepository
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
