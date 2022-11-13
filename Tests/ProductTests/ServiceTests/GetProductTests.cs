using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ProductTests.ServiceTests
{
    public class GetProductTests : TestProductServices
    {
        [Fact]
        public async Task GetProducts_Success()
        {
            //Arrange
            //Act
            var productDtoList = await Service.Get();
            //Assert
            Assert.Equal(2, productDtoList.Count);
        }
    }
}
