using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly WebSkladDbContext _context;

        public CategoryRepository(WebSkladDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == id);
            return category;
        }

        public void Create(Category item)
        {
            _context.Categories.Add(item);
        }

        public void Update(Category item)
        {
            _context.Categories.Update(item);
        }

        public void Delete(Category item)
        {
            _context.Categories.Remove(item);
        }

        public bool IsExist(Guid id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }
    }
}
