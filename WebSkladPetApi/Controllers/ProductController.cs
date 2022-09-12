using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _service.Get();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductDTO productDto)
        {
            await _service.Create(productDto, CancellationToken.None);
            return Ok();
        }

    }

}
