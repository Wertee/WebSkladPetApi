using Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Outcome.DTO;

namespace Application.Interfaces
{
    public interface IOutcomeService
    {
        Task<List<OutcomeDTO>> Get();
        Task<OutcomeDTO> Get(Guid id);
        Task Create(OutcomeDTO productDto);
        Task Update(OutcomeDTO productDto);
        Task Delete(Guid id);
    }
}
