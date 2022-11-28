using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Outcome.DTO;
using Application.Product.Validation;
using AutoMapper;

namespace Application.Outcome.Services
{
    public class OutcomeService : IOutcomeService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public OutcomeService(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<List<OutcomeDTO>> Get()
        {
            var outcomes = await _uof.OutcomeRepository.GetAllAsync();
            var outcomesDto = _mapper.Map<List<Domain.Entity.Outcome>, List<OutcomeDTO>>(outcomes);
            return outcomesDto;
        }

        public async Task<OutcomeDTO> Get(Guid id)
        {
            var outcome = await _uof.OutcomeRepository.GetByIdAsync(id);
            if (outcome == null)
                throw new Exception();
            var outcomeDto = _mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);
            return outcomeDto;

        }

        public async Task Create(OutcomeDTO outcomeDto)
        {
            var product = await _uof.ProductRepository.GetByIdAsync(outcomeDto.ProductId);
            if (product == null)
                throw new ProductNotFoundException("Материал не найден");

            product.Count -= outcomeDto.Count;
            product.CountToGive = (product.CountToGive - outcomeDto.Count) < 0 ? product.CountToGive = 0 : product.CountToGive - outcomeDto.Count;
            if (product.CountToGive == 0)
                product.CanBeGiven = false;
            var updateProductValidation = new CreateProductValidation(product);
            updateProductValidation.ValidateCount();
            var outcome = _mapper.Map<OutcomeDTO, Domain.Entity.Outcome>(outcomeDto);
            _uof.OutcomeRepository.Create(outcome);
            _uof.ProductRepository.Update(product);
            await _uof.SaveAsync();
        }

        public async Task Update(OutcomeDTO outcomeDto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
