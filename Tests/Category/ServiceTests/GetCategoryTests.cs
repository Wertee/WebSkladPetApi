using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;
using Application.Exceptions;
using Moq;

namespace Tests.Category.ServiceTests
{
    public class GetCategoryTests : CategoryTestsBase
    {
        [Fact]
        public async Task GetAll_Success()
        {
            //Arrange
            List<Domain.Entity.Category> categories = new List<Domain.Entity.Category>()
            {
                new Domain.Entity.Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestCategory"
                },
                new Domain.Entity.Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestCategory1"
                }
            };
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

            //Act
            var categoriesDto = await _service.Get();

            //Assert
            Assert.Equal(2, categoriesDto.Count);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var category = new Domain.Entity.Category()
            {
                Id = categoryId,
                Name = "TestCategory"
            };
            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);

            //Act
            var categoryDto = await _service.Get(categoryId);

            //Assert
            Assert.Equal(categoryId, categoryDto.Id);
        }

        [Fact]
        public async Task GetByIdAsync_FailOnWrongCategoryId()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            //Assert
            await Assert.ThrowsAsync<CategoryNotFoundException>(async () => await _service.Get(categoryId));
        }
    }
}
