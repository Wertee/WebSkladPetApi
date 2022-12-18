﻿using Application.Category.DTO;
using Application.Category.Validation;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;

namespace Application.Category.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Get()
        {
            var category = await _uof.CategoryRepository.GetAllAsync();
            var categoryDto = _mapper.Map<List<Domain.Entity.Category>, List<CategoryDTO>>(category);
            return categoryDto;
        }

        public async Task<CategoryDTO> Get(Guid id)
        {
            var category = await _uof.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new CategoryNotFoundException("Not found");
            var categoryDto = _mapper.Map<Domain.Entity.Category, CategoryDTO>(category);
            return categoryDto;
        }

        public async Task Create(CategoryDTO categoryDto)
        {
            //validation
            var categoryValidation = new CategoryValidation(categoryDto);
            categoryValidation.ValidateName();
            var category = _mapper.Map<CategoryDTO, Domain.Entity.Category>(categoryDto);
            _uof.CategoryRepository.Create(category);
            await _uof.SaveAsync();
        }

        public async Task Update(CategoryDTO categoryDto)
        {
            //validation
            var categoryValidation = new CategoryValidation(categoryDto);
            categoryValidation.ValidateName();
            var category = _mapper.Map<CategoryDTO, Domain.Entity.Category>(categoryDto);
            _uof.CategoryRepository.Update(category);
            await _uof.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var category = await _uof.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new CategoryNotFoundException("Not found");
            _uof.CategoryRepository.Delete(category);
            await _uof.SaveAsync();
        }
    }
}
