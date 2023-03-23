using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Product.Services;
using Tests.Common;

namespace Tests.ProductTests.ServiceTests
{
    public class DeleteProductTests : TestProductServices
    {
        [Fact]
        public async Task DeleteProduct_Success()
        {
            //Arrange
            //Act
            await Service.DeleteAsync(ProductsContextFactory.ProductIdForDelete);

            //Assert
            Assert.Null(Context.Products.SingleOrDefault(x => x.Id == ProductsContextFactory.ProductIdForDelete));
        }
        [Fact]
        public async Task DeleteProduct_FailOnWrongId()
        {
            //Arrange
            //Act
            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await Service.DeleteAsync(Guid.NewGuid()));
        }
    }
}
