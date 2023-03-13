using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Product.DTO;
using Application.Product.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Common;

namespace Tests.ProductTests.ServiceTests
{
    public class UpdateProductTests : TestProductServices
    {
        [Fact]
        public async Task UpdateProduct_Success()
        {
            //Arrange
            string newDescription = "Test description";

            var updatedProductDTO = new ProductDTO()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("5502E6E8-02CB-43C2-B777-8AB395FEBCC9"),
                Count = 5,
                Description = newDescription,
                Id = Guid.Parse("E8582C8E-3099-487D-9AC8-B30E9A40FF30"),
                Name = "Клавиатура",
                CategoryName = "Keyboard"
            };
            Context.ChangeTracker.Clear();
            //Act
            await Service.UpdateAsync(updatedProductDTO);

            //Assert
            Assert.True(Context.Products.Any(x => x.Id == ProductsContextFactory.ProductIdForUpdate && x.Description == newDescription));
        }
        [Fact]
        public async Task UpdateProduct_FailOnWrongId()
        {
            //Arrange
            string newDescription = "Test description";

            var updatedProductDTO = new ProductDTO()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("5502E6E8-02CB-43C2-B777-8AB395FEBCC9"),
                Count = 5,
                Description = newDescription,
                Id = Guid.NewGuid(),
                Name = "Клавиатура",
                CategoryName = "Keyboard"
            };
            Context.ChangeTracker.Clear();
            //Act
            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await Service.UpdateAsync(updatedProductDTO));
        }

        [Fact]
        public async Task UpdateProduct_FailCountLowerZero()
        {
            string newDescription = "Test description";

            var updatedProductDTO = new ProductDTO()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("5502E6E8-02CB-43C2-B777-8AB395FEBCC9"),
                Count = -1,
                Description = newDescription,
                Id = Guid.Parse("E8582C8E-3099-487D-9AC8-B30E9A40FF30"),
                Name = "Клавиатура",
                CategoryName = "Keyboard"
            };
            Context.ChangeTracker.Clear();
            //Act
            //Assert
            await Assert.ThrowsAsync<ProductValidationException>(async () => await Service.UpdateAsync(updatedProductDTO));
        }
    }
}
