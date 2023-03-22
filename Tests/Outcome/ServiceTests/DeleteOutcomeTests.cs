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
            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 2,
                ProductId = Guid.NewGuid(),
                ProductName = "Test",
                OutcomeDate = DateTime.Now,
                Recipient = "Test"
            };

            var product = new Product()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                Count = 5,
                Description = "Мышь Оклик",
                Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                Name = "Мышь"
            };

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(outcome);

            //Act
            var taskResult = Service.DeleteAsync(outcome.Id).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
            Assert.Equal(7, product.Count);
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongOutcomeId()
        {
            //Arrange
            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 2,
                ProductId = Guid.NewGuid(),
                ProductName = "Test",
                OutcomeDate = DateTime.Now,
                Recipient = "Test"
            };

            var product = new Product()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                Count = 5,
                Description = "Мышь Оклик",
                Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                Name = "Мышь"
            };

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);

            //Act
            //Assert
            await Assert.ThrowsAsync<OutcomeNotFoundException>(async () => await Service.DeleteAsync(outcomeDto.Id));
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongProductId()
        {
            //Arrange
            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 2,
                ProductId = Guid.NewGuid(),
                ProductName = "Test",
                OutcomeDate = DateTime.Now,
                Recipient = "Test"
            };

            var product = new Product()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                Count = 5,
                Description = "Мышь Оклик",
                Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                Name = "Мышь"
            };

            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(outcome);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);

            //Act
            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await Service.DeleteAsync(outcomeDto.Id));

        }
    }
}
