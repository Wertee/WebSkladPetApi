using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Product.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IWebSkladDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(IWebSkladDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> Get()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            var productsDto = _mapper.Map<List<Domain.Entity.Product>, List<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<ProductDTO> Get(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
            var productDto = _mapper.Map<Domain.Entity.Product, ProductDTO>(product);
            return productDto;
        }


        public async Task Create(ProductDTO productDto, CancellationToken token)
        {
            //validation

            var product = _mapper.Map<ProductDTO, Domain.Entity.Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync(token);
        }

        public Task Update(ProductDTO productDto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
