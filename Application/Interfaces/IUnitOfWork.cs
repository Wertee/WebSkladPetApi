using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Domain.Entity.Product> ProductRepository { get; }
        IRepository<Domain.Entity.Category> CategoryRepository { get; }
        IRepository<Domain.Entity.Outcome> OutcomeRepository { get; }
        Task SaveAsync();
    }
}
