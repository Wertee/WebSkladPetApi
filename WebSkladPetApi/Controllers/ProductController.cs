using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSkladPetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly WebSkladDbContext _context;

        public ProductController(WebSkladDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _context.Products.Include(c => c.Category).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductDTO productDto)
        {
            var prod = new Product()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                CanBeGiven = productDto.CanBeGiven,
                Count = productDto.Count,
                CategoryId = productDto.CategoryId,
            };

            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }

    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeGiven { get; set; }
        public int Count { get; set; }
        public Guid CategoryId { get; set; }
    }
}
