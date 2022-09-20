﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;
using Application.Category.Validation;
using Application.Interfaces;
using AutoMapper;

namespace Application.Category.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Domain.Entity.Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Domain.Entity.Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Get()
        {
            var category = await _repository.GetAll();
            var categoryDto = _mapper.Map<List<Domain.Entity.Category>, List<CategoryDTO>>(category);
            return categoryDto;
        }

        public async Task<CategoryDTO> Get(Guid id)
        {
            var category = await _repository.Get(id);
            if (category == null)
                throw new CategoryNotFoundException("Not found");
            var categoryDto = _mapper.Map<Domain.Entity.Category, CategoryDTO>(category);
            return categoryDto;
        }

        public async Task Create(CategoryDTO categoryDto)
        {
            //validation

            var category = _mapper.Map<CategoryDTO, Domain.Entity.Category>(categoryDto);
            await _repository.Create(category);
        }

        public async Task Update(CategoryDTO categoryDto)
        {
            //validation

            var category = _mapper.Map<CategoryDTO, Domain.Entity.Category>(categoryDto);
            await _repository.Update(category);
        }

        public async Task Delete(Guid id)
        {
            var category = await _repository.Get(id);
            if (category == null)
                throw new CategoryNotFoundException("Not found");
            await _repository.Delete(category);
        }
    }
}