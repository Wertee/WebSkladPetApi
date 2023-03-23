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
            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 2,
                ProductId = Guid.NewGuid(),
                Product = new Product(),
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
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.IsExist(It.IsAny<Guid>())).Returns(true);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(outcome);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.IsExist(It.IsAny<Guid>())).Returns(true);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);

            //Act
            var taskResult = Service.UpdateAsync(outcomeDto).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongOutcomeId()
        {
            //Arrange

            var product = new Product()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                Count = 5,
                Description = "Мышь Оклик",
                Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                Name = "Мышь"
            };

            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 2,
                ProductId = Guid.NewGuid(),
                Product = product,
                OutcomeDate = DateTime.Now,
                Recipient = "Test"
            };

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.IsExist(It.IsAny<Guid>())).Returns(true);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(outcome);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.IsExist(It.IsAny<Guid>())).Returns(false);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);

            //Act
            //Assert
            await Assert.ThrowsAsync<OutcomeNotFoundException>(async () => await Service.UpdateAsync(outcomeDto));
        }

        [Fact]
        public async Task UpdateAsync_FailOnWrongProductId()
        {
            //Arrange
            var product = new Product()
            {
                CanBeGiven = true,
                CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                Count = 5,
                Description = "Мышь Оклик",
                Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                Name = "Мышь"
            };

            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 2,
                ProductId = Guid.NewGuid(),
                Product = product,
                OutcomeDate = DateTime.Now,
                Recipient = "Test"
            };

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository.IsExist(It.IsAny<Guid>())).Returns(false);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(outcome);
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.IsExist(It.IsAny<Guid>())).Returns(true);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);

            //Act
            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await Service.UpdateAsync(outcomeDto));
        }
    }
}
