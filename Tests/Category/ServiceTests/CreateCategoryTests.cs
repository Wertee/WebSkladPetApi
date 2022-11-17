using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;
using Domain.Entity;
using Moq;

namespace Tests.Category.ServiceTests
{
    public class CreateCategoryTests : CategoryTestsBase
    {
        [Fact]
        public async Task CreateAsync_Success()
        {
            //Arrange
            var category = new Domain.Entity.Category()
            {
                Id = Guid.NewGuid(),
                Name = "TestCategory"
            };

            var categoryDto = _mapper.Map<Domain.Entity.Category, CategoryDTO>(category);


            _categoryRepositoryMock.Setup(x => x.CreateAsync(category))
                .Returns(Task.CompletedTask);
            //Act
            var taskResult = _service.Create(categoryDto).IsCompletedSuccessfully;
            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
        }
        [Fact]
        public async Task CreateAsync_FailOnShortCategoryName()
        {
            //Arrange
            var category = new Domain.Entity.Category()
            {
                Id = Guid.NewGuid(),
                Name = "T"
            };
            var categoryDto = _mapper.Map<Domain.Entity.Category, CategoryDTO>(category);
            _categoryRepositoryMock.Setup(x => x.CreateAsync(category))
                .Returns(Task.CompletedTask);
            //Act
            var taskResult = _service.Create(categoryDto).IsCompletedSuccessfully;
            //Assert
            Assert.Equal(Task.CompletedTask.IsFaulted, taskResult);
        }
    }
}
