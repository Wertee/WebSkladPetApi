using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mapping;
using Application.Product.DTO;
using Application.Product.Services;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Tests.Common;

namespace Tests.ProductTests.ServiceTests
{
    public class CreateProductTests : TestProductServices
    {
        [Fact]
        public async Task CreateProduct_Success()
        {
            //Arrange
            ProductDTO newProductDto = new ProductDTO()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                Count = 5,
                Description = "Мышь Logitech",
                CategoryName = "Mouse",
                //Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                Name = "Мышь Log"
            };

            //Act
            await Service.CreateAsync(newProductDto);

            //Assert
            Assert.NotNull(Context.Products.SingleOrDefault(x => x.Name == newProductDto.Name && x.Description == newProductDto.Description));
        }
    }
}
