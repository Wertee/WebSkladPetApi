using Application.Exceptions;
using Application.Outcome.DTO;
using Domain.Entity;
using Moq;

namespace Tests.Outcome.ServiceTests
{
    public class DeleteOutcomeTests : OutcomeTestsBase
    {
        [Fact]
        public void DeleteAsync_Success()
        {
            //Arrange

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ProductField);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(OutcomeField);

            //Act
            var taskResult = Service.DeleteAsync(OutcomeField.Id).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
            Assert.Equal(7, ProductField.Count);
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongOutcomeId()
        {
            //Arrange

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ProductField);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);

            //Act
            //Assert
            await Assert.ThrowsAsync<OutcomeNotFoundException>(async () => await Service.DeleteAsync(outcomeDto.Id));
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongProductId()
        {
            //Arrange

            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(OutcomeField);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(OutcomeField);

            //Act
            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await Service.DeleteAsync(outcomeDto.Id));

        }
    }
}
