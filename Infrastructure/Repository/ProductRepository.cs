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

        public ProductRepository(WebSkladDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return products;
        }

        public async Task<Product> Get(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
            return product;
        }

        public async Task Create(Product item)
        {
            _context.Products.Add(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Update(Product item)
        {
            _context.Products.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
