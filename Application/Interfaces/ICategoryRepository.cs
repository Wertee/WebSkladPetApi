using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Domain.Entity.Category>> GetAll();
        Task<Domain.Entity.Category> Get(Guid id);
        Task Create(Domain.Entity.Category category);
        Task Update(Domain.Entity.Category category);
        Task Delete(Domain.Entity.Category category);
    }
}
