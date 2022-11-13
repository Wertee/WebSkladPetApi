using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class OutcomeRepository : IOutcomeRepository
    {
        private readonly WebSkladDbContext _context;

        public OutcomeRepository(WebSkladDbContext context)
        {
            _context = context;
        }

        public async Task<List<Outcome>> GetAll()
        {
            var outcomes = await _context.Outcomes.ToListAsync();
            return outcomes;
        }

        public async Task<Outcome> Get(Guid id)
        {
            var outcome = await _context.Outcomes.FirstOrDefaultAsync(x => x.Id == id);
            return outcome;
        }

        public async Task Create(Outcome item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Outcome item)
        {
            _context.Outcomes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Outcome item)
        {
            _context.Outcomes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public bool IsExist(Guid id)
        {
            return _context.Outcomes.Any(x => x.Id == id);
        }
    }
}
