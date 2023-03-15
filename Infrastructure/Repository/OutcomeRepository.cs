using Application.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class OutcomeRepository : IRepository<Outcome>
    {
        private readonly WebSkladDbContext _context;

        public OutcomeRepository(WebSkladDbContext context)
        {
            _context = context;
        }

        public async Task<List<Outcome>> GetAllAsync()
        {
            var outcomes = await _context.Outcomes.ToListAsync();
            return outcomes;
        }

        public async Task<Outcome> GetByIdAsync(Guid id)
        {
            var outcome = await _context.Outcomes.FirstOrDefaultAsync(x => x.Id == id);
            return outcome;
        }

        public void Create(Outcome item)
        {
            _context.Add(item);
        }

        public void Update(Outcome item)
        {
            _context.Outcomes.Update(item);
        }

        public void Delete(Outcome item)
        {
            _context.Outcomes.Remove(item);
        }

        public bool IsExist(Guid id)
        {
            return _context.Outcomes.Any(x => x.Id == id);
        }

        public void Detach(Outcome entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
