using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Outcome.DTO;
using AutoMapper;

namespace Application.Outcome.Services
{
    public class OutcomeService : IOutcomeService
    {
        private readonly IOutcomeRepository _repository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OutcomeService(IOutcomeRepository repository, IProductService productService, IMapper mapper)
        {
            _repository = repository;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<List<OutcomeDTO>> Get()
        {
            var outcomes = await _repository.GetAll();
            var outcomesDto = _mapper.Map<List<Domain.Entity.Outcome>, List<OutcomeDTO>>(outcomes);
            return outcomesDto;
        }

        public async Task<OutcomeDTO> Get(Guid id)
        {
            var outcome = await _repository.Get(id);
            if (outcome == null)
                throw new Exception();
            var outcomeDto = _mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);
            return outcomeDto;

        }

        public Task Create(OutcomeDTO productDto)
        {
            throw new NotImplementedException();
        }

        public Task Update(OutcomeDTO productDto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
