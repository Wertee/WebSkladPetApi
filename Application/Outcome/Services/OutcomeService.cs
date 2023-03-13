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

        public async Task<List<OutcomeDTO>> GetAllAsync()
        {
            var outcomes = await _uof.OutcomeRepository.GetAllAsync();
            var outcomesDto = _mapper.Map<List<Domain.Entity.Outcome>, List<OutcomeDTO>>(outcomes);
            return outcomesDto;
        }

        public async Task<OutcomeDTO> GetByIdAsync(Guid id)
        {
            var outcome = await _uof.OutcomeRepository.GetByIdAsync(id);
            if (outcome == null)
                throw new Exception();
            var outcomeDto = _mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);
            return outcomeDto;

        }

        public async Task CreateAsync(OutcomeDTO outcomeDto)
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

        public async Task UpdateAsync(OutcomeDTO outcomeDto)
        {
            var outcome = _mapper.Map<OutcomeDTO, Domain.Entity.Outcome>(outcomeDto);
            if (_uof.OutcomeRepository.IsExist(outcome.Id))
                throw new OutcomeNotFoundException("Объект не найден");
            if (_uof.ProductRepository.IsExist(outcome.ProductId))
                throw new ProductNotFoundException("Материал не найден");

            var product = await _uof.ProductRepository.GetByIdAsync(outcome.Id);
            product.Count -= outcomeDto.Count;
            product.CountToGive = (product.CountToGive - outcomeDto.Count) < 0 ? product.CountToGive = 0 : product.CountToGive - outcomeDto.Count;
            if (product.CountToGive == 0)
                product.CanBeGiven = false;
            var updateProductValidation = new UpdateProductValidation(product);
            updateProductValidation.ValidateCount();
            _uof.OutcomeRepository.Update(outcome);
            _uof.ProductRepository.Update(product);
            await _uof.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var outcome = await _uof.OutcomeRepository.GetByIdAsync(id);
            if (outcome == null)
                throw new OutcomeNotFoundException("Объект не найден");
            var product = await _uof.ProductRepository.GetByIdAsync(outcome.ProductId);
            if (product == null)
                throw new ProductNotFoundException("Материал не найден");
            product.Count += outcome.Count;
            _uof.ProductRepository.Update(product);
            _uof.OutcomeRepository.Delete(outcome);
            await _uof.SaveAsync();
        }
    }
}
