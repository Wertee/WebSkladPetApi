﻿using Application.Category.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebSkladPetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var categoryDto = await _service.GetByIdAsync(id);
                return Ok(categoryDto);
            }
            catch (CategoryNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryDTO categoryDto)
        {
            await _service.CreateAsync(categoryDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryDTO categoryDto)
        {
            await _service.UpdateAsync(categoryDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (CategoryValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
