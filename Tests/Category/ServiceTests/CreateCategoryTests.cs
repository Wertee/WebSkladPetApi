using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;
using Application.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Category.ServiceTests
{

    public class CreateCategoryTests : CategoryTestsBase
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        [Fact]
        public async Task CreateAsync_Success()
        {
            //Arrange
            var category = new Domain.Entity.Category()
            {
                Id = Guid.NewGuid(),
                Name = "TestCategory"
            };

            var categoryDto = Mapper.Map<Domain.Entity.Category, CategoryDTO>(category);


            _unitOfWorkMock.Setup(x => x.SaveAsync())
                .Returns(Task.CompletedTask);
            //Act
            var taskResult = Service.CreateAsync(categoryDto).IsCompletedSuccessfully;
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
            var categoryDto = Mapper.Map<Domain.Entity.Category, CategoryDTO>(category);
            _unitOfWorkMock.Setup(x => x.SaveAsync())
                .Returns(Task.CompletedTask);
            //Act
            var taskResult = Service.CreateAsync(categoryDto).IsCompletedSuccessfully;
            //Assert
            Assert.Equal(Task.CompletedTask.IsFaulted, taskResult);
        }
    }
}
