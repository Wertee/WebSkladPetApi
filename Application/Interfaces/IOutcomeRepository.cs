using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOutcomeRepository
    {
        Task<List<Domain.Entity.Outcome>> GetAll();
        Task<Domain.Entity.Outcome> Get(Guid id);
        Task Create(Domain.Entity.Outcome outcome);
        Task Update(Domain.Entity.Outcome outcome);
        Task Delete(Domain.Entity.Outcome outcome);
    }
}
