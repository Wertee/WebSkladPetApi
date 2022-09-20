using Application.Category.DTO;
using Application.Category.Validation;
using Application.Interfaces;
using Domain.Entity;
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
            var categories = await _service.Get();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var categoryDto = await _service.Get(id);
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
            //validation

            await _service.Create(categoryDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryDTO categoryDto)
        {
            await _service.Update(categoryDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
