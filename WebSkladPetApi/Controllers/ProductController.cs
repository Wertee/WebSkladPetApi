using Application.Exceptions;
using Application.Interfaces;
using Application.Product.DTO;
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
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var productsDto = await _service.Get();
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(Guid id)
        {
            try
            {
                var productDto = await _service.Get(id);
                return Ok(productDto);
            }
            catch (ProductNotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductDTO productDto)
        {
            try
            {
                await _service.Create(productDto);
                return Ok();
            }
            catch (ProductValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.Update(productDto);
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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (ProductNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }



    }

}
