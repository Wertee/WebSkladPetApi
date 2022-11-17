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
    public class CategoryRepository : ICategoryRepository
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

        public async Task CreateAsync(Category item)
        {
            _context.Categories.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category item)
        {
            _context.Categories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category item)
        {
            _context.Categories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public bool IsExist(Guid id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }
    }
}
