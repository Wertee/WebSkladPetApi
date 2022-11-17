using Application.Category.DTO;
using Application.Category.Validation;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;

namespace Application.Category.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Get()
        {
            var category = await _repository.GetAllAsync();
            var categoryDto = _mapper.Map<List<Domain.Entity.Category>, List<CategoryDTO>>(category);
            return categoryDto;
        }

        public async Task<CategoryDTO> Get(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new CategoryNotFoundException("Not found");
            var categoryDto = _mapper.Map<Domain.Entity.Category, CategoryDTO>(category);
            return categoryDto;
        }

        public async Task Create(CategoryDTO categoryDto)
        {
            //validation
            var categoryValidation = new CreateCategoryValidation(categoryDto);
            categoryValidation.Validate();
            var category = _mapper.Map<CategoryDTO, Domain.Entity.Category>(categoryDto);
            await _repository.CreateAsync(category);
        }

        public async Task Update(CategoryDTO categoryDto)
        {
            //validation
            var categoryValidation = new CategoryValidation(categoryDto);
            categoryValidation;
            var category = _mapper.Map<CategoryDTO, Domain.Entity.Category>(categoryDto);
            await _repository.UpdateAsync(category);
        }

        public async Task Delete(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new CategoryNotFoundException("Not found");
            await _repository.DeleteAsync(category);
        }
    }
}
