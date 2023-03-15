using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;
using Application.Exceptions;

namespace Tests.Category.ServiceTests
{
    public class UpdateCategoryTests : CategoryTestsBase
    {
        [Fact]
        public async Task UpdateAsync_Success()
        {
            //Arrange
            var updatedCategory = new Domain.Entity.Category()
            {
                Id = new Guid(),
                Name = "UpdatedCategory"
            };

            var updatedCategoryDto = Mapper.Map<Domain.Entity.Category, CategoryDTO>(updatedCategory);

            //Act

            var taskResult = Service.UpdateAsync(updatedCategoryDto).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
        }

        [Fact]
        public async Task UpdateAsync_FailOnShortCategoryName()
        {
            //Arrange
            var updatedCategory = new Domain.Entity.Category()
            {
                Id = new Guid(),
                Name = "Up"
            };
            var updatedCategoryDto = Mapper.Map<Domain.Entity.Category, CategoryDTO>(updatedCategory);

            //Act
            //Assert
            await Assert.ThrowsAsync<CategoryValidationException>(async () => await Service.UpdateAsync(updatedCategoryDto));
        }
    }
}
