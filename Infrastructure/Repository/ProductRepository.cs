using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Product.DTO;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly WebSkladDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(WebSkladDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
            return product;
        }

        public void Create(Product item)
        {
            _context.Products.Add(item);
        }

        public void Update(Product item)
        {
            _context.Products.Update(item);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public bool IsExist(Guid id)
        {
            return _context.Products.Any(x => x.Id == id);
        }
    }
}
