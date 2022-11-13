using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mapping;
using Application.Interfaces;
using Application.Product.Services;
using AutoMapper;
using Domain.Entity;
using Moq;

namespace Tests.ProductTests.RepositoryTests
{
    public class MockProductsTest
    {

        [Fact]
        public async Task GetProducts()
        {
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });

            var items = new List<Product>()
            {
                new Product()
                {
                    CanBeGiven = true,
                    CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                    Count = 5,
                    Description = "Мышь Оклик",
                    Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                    Name = "Мышь"
                },
                new Product()
                {
                    CanBeGiven = true,
                    CategoryId = Guid.Parse("5502E6E8-02CB-43C2-B777-8AB395FEBCC9"),
                    Count = 5,
                    Description = "Клавиатура Оклик",
                    Id = Guid.Parse("E8582C8E-3099-487D-9AC8-B30E9A40FF30"),
                    Name = "Клавиатура"
                }
            };

            mockProductRepository.Setup(m => m.GetAll()).ReturnsAsync(items.AsEnumerable().ToList());

            ProductService service = new ProductService(mockMapper.CreateMapper(), mockProductRepository.Object);

            var result = await service.Get();

            Assert.Equal(2, result.Count);


        }
    }
}
