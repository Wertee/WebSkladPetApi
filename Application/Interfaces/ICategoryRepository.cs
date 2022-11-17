using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Domain.Entity.Category>> GetAllAsync();
        Task<Domain.Entity.Category> GetByIdAsync(Guid id);
        Task CreateAsync(Domain.Entity.Category category);
        Task UpdateAsync(Domain.Entity.Category category);
        Task DeleteAsync(Domain.Entity.Category category);

    }
}
