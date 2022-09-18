using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Product.DTO;
using Application.Product.Services;
using Domain.Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var productDto = await _service.Get(id);
            return Ok(productDto);
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
            try
            {
                await _service.Update(productDto);
                return Ok();
            }
            catch (ProductValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }



    }

}
