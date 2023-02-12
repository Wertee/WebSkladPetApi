using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entity;
using Moq;

namespace Tests.Category.ServiceTests
{
    public class DeleteCategoryTests : CategoryTestsBase
    {
        [Fact]
        public void DeleteAsync_Success()
        {
            //Arrange
            var category = new Domain.Entity.Category()
            {
                Id = Guid.NewGuid(),
                Name = "Test category"
            };

            List<Product> products = new()
            {
                new Product()
                {
                    CanBeGiven = true,
                    CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                    Count = 5,
                    Description = "Мышь Оклик",
                    Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                    Name = "Мышь"
                }
            };
            UnitOfWorkMock.Setup(uow => uow.CategoryRepository.GetByIdAsync(category.Id)).ReturnsAsync(category);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetAllAsync()).ReturnsAsync(products);
            //Act
            var taskResult = Service.DeleteAsync(category.Id).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
        }
        [Fact]
        public async Task DeleteAsync_FailOnWrongCategoryId()
        {
            //Arrange
            var category = new Domain.Entity.Category()
            {
                Id = Guid.NewGuid(),
                Name = "Test category"
            };

            UnitOfWorkMock.Setup(uow => uow.CategoryRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            //Assert
            await Assert.ThrowsAsync<CategoryNotFoundException>(async () => await Service.GetByIdAsync(category.Id));
        }

        [Fact]
        public async Task DeleteAsync_FailOnProductsLinkedToCategory()
        {
            //Arrange
            var category = new Domain.Entity.Category()
            {
                Id = Guid.NewGuid(),
                Name = "Test category"
            };

            List<Product> products = new()
            {
                new Product()
                {
                    CanBeGiven = true,
                    CategoryId = category.Id,
                    Count = 5,
                    Description = "Мышь Оклик",
                    Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                    Name = "Мышь"
                }
            };
            UnitOfWorkMock.Setup(uow => uow.CategoryRepository.GetByIdAsync(category.Id)).ReturnsAsync(category);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetAllAsync()).ReturnsAsync(products);

            //Act
            //Assert
            await Assert.ThrowsAsync<CategoryValidationException>(async () => await Service.DeleteAsync(category.Id));
        }
    }
}
