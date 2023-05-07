using Application.Exceptions;
using Application.Interfaces;
using Application.Product.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSkladPetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var productsDto = await _service.GetAllAsync();
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ProductDTO>> Get(Guid id)
        {
            try
            {
                var productDto = await _service.GetByIdAsync(id);
                return Ok(productDto);
            }
            catch (ProductNotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post(ProductDTO productDto)
        {
            try
            {
                await _service.CreateAsync(productDto);
                return Ok();
            }
            catch (ProductValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put(ProductDTO productDto)
        {
            try
            {
                await _service.UpdateAsync(productDto);
                return Ok();
            }
            catch (ProductValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ProductNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (ProductNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }

}
