using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Category.ServiceTests
{
    public class UpdateCategoryTests
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
        }
    }
}
