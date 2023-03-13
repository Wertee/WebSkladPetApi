using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Outcome.DTO;
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

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);


            //Act
            var taskResult = Service.CreateAsync(outcomeDto).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, taskResult);
        }

        [Fact]
        public void CreateAsync_FailOnProductIsNull()
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

            UnitOfWorkMock.Setup(uow => uow.ProductRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);

            //Act
            var taskResult = Service.CreateAsync(outcomeDto).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsFaulted, taskResult);
        }

        [Fact]
        public void CreateAsync_FailOnWrongProductCount()
        {
            //Arrange
            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 10, //More than products
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

            var outcomeDto = Mapper.Map<Domain.Entity.Outcome, OutcomeDTO>(outcome);

            //Act
            var taskResult = Service.CreateAsync(outcomeDto).IsCompletedSuccessfully;

            //Assert
            Assert.Equal(Task.CompletedTask.IsFaulted, taskResult);
        }
    }
}
