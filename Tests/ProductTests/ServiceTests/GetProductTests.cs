using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mapping;
using Application.Exceptions;
using Application.Interfaces;
using Application.Product.DTO;
using Application.Product.Services;
using AutoMapper;
using Domain.Entity;
using Moq;

namespace Tests.ProductTests.ServiceTests
{
    public class GetProductTests : TestProductServices
    {
        private readonly ProductService _service;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper _mapper;

        public GetProductTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            _mapper = mapperConfiguration.CreateMapper();
            _service = new ProductService(_mapper, _unitOfWorkMock.Object);
        }
        [Fact]
        public async Task GetAllProducts_Success()
        {
            //Arrange
            //Act
            var productDtoList = await Service.GetAllAsync();
            //Assert
            Assert.Equal(2, productDtoList.Count);
        }
        [Fact]
        public async Task GetAllProductsUsingMock_Success()
        {
            //Arrange
            List<Product> mockProducts = new List<Product>()
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
                },
                new Product()
                {
                    CanBeGiven = true,
                    CategoryId = Guid.Parse("5502E6E8-02CB-43C2-B777-8AB395FEBC22"),
                    Count = 3,
                    Description = "Клавиатура Оклик 123",
                    Id = Guid.Parse("E8582C8E-3099-487D-9AC8-B30E9A40FF30"),
                    Name = "Клавиатура"
                }
            };

            _unitOfWorkMock.Setup(x => x.ProductRepository.GetAllAsync()).ReturnsAsync(mockProducts);

            //Act
            var products = await _service.GetAllAsync();

            //Assert
            Assert.Equal(3, products.Count);
        }
        [Fact]
        public async Task GetByIdAsync_Success()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var product = new Product()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA000"),
                Count = 5,
                Description = "Мышь Оклик",
                Id = productId,
                Name = "Мышь",
            };
            _unitOfWorkMock.Setup(x => x.ProductRepository.GetByIdAsync(productId)).ReturnsAsync(product);
            //Act
            var productDto = await _service.GetByIdAsync(productId);
            //Assert
            Assert.Equal(productId, productDto.Id);

        }
        [Fact]
        public async Task GetByIdAsync_FailOnWrongProductId()
        {
            //Arrange
            var productId = Guid.NewGuid();
            _unitOfWorkMock.Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await _service.GetByIdAsync(productId));
        }
    }
}
