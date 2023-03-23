using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Outcome.DTO;
using Application.Product.Validation;
using Domain.Entity;
using Moq;

namespace Tests.Outcome.ServiceTests
{
    public class CreateOutcomeTests : OutcomeTestsBase
    {
        [Fact]
        public void CreateAsync_Success()
        {
            //Arrange


            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ProductField);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);


            //Act
            var taskResult = Service.CreateAsync(outcomeDto).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
        }

        [Fact]
        public void CreateAsync_FailOnProductIsNull()
        {
            //Arrange


            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);

            //Act
            //Assert
            Assert.ThrowsAsync<ProductNotFoundException>(async () => await Service.CreateAsync(outcomeDto));
        }

        [Fact]
        public void CreateAsync_FailOnWrongProductCount()
        {
            //Arrange

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ProductField);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);

            //Act
            //Assert
            Assert.ThrowsAsync<ProductValidationException>(async () => await Service.CreateAsync(outcomeDto));
        }
    }
}
