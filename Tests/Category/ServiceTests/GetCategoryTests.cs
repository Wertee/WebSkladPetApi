using Application.Exceptions;
using Application.Interfaces;
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
                },
                new Domain.Entity.Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestCategory1"
                },
                new Domain.Entity.Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestCategory1"
                },
                new Domain.Entity.Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestCategory1"
                },
                new Domain.Entity.Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestCategory1"
                }
            };
            _unitOfWorkMock.Setup(x => x.CategoryRepository.GetAllAsync()).ReturnsAsync(categories);

            //Act
            var categoriesDto = await _service.GetAllAsync();

            //Assert
            Assert.Equal(categories.Count, categoriesDto.Count);
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
            _unitOfWorkMock.Setup(x => x.CategoryRepository.GetByIdAsync(categoryId)).ReturnsAsync(category);

            //Act
            var categoryDto = await _service.GetByIdAsync(categoryId);

            //Assert
            Assert.Equal(categoryId, categoryDto.Id);
        }

        [Fact]
        public async Task GetByIdAsync_FailOnWrongCategoryId()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            _unitOfWorkMock.Setup(x => x.CategoryRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            //Assert
            await Assert.ThrowsAsync<CategoryNotFoundException>(async () => await _service.GetByIdAsync(categoryId));
        }
    }
}
