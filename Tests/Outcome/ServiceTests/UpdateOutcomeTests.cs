using Application.Outcome.DTO;
using Domain.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Tests.Outcome.ServiceTests
{
    public class UpdateOutcomeTests : OutcomeTestsBase
    {
        [Fact]
        public void UpdateAsync_Succsess()
        {
            //Arrange

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ProductField);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.IsExist(It.IsAny<Guid>())).Returns(true);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(OutcomeField);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.IsExist(It.IsAny<Guid>())).Returns(true);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);

            //Act
            var taskResult = Service.UpdateAsync(outcomeDto).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongOutcomeId()
        {
            //Arrange

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ProductField);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.IsExist(It.IsAny<Guid>())).Returns(true);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(OutcomeField);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.IsExist(It.IsAny<Guid>())).Returns(false);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);

            //Act
            //Assert
            await Assert.ThrowsAsync<OutcomeNotFoundException>(async () => await Service.UpdateAsync(outcomeDto));
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongProductId()
        {
            //Arrange

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ProductField);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.IsExist(It.IsAny<Guid>())).Returns(false);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(OutcomeField);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.IsExist(It.IsAny<Guid>())).Returns(true);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);

            //Act
            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await Service.UpdateAsync(outcomeDto));
        }
    }
}
