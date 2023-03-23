using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Moq;

namespace Tests.Outcome.ServiceTests
{
    public class GetOutcomeTests : OutcomeTestsBase
    {
        [Fact]
        public async Task GetAllAsync_Succsess()
        {
            //Arrange
            List<Domain.Entity.Outcome> outcomes = new List<Domain.Entity.Outcome>()
            {
                new Domain.Entity.Outcome()
                {
                    Id = Guid.NewGuid(),
                    Count = 2,
                    OutcomeDate = DateTime.Now,
                    ProductId = Guid.NewGuid(),
                    ProductName = "Mouse",
                    Recipient = "Admin"
                },
                new Domain.Entity.Outcome()
                {
                    Id = Guid.NewGuid(),
                    Count = 3,
                    OutcomeDate = DateTime.Now,
                    ProductId = Guid.NewGuid(),
                    ProductName = "Keyboard",
                    Recipient = "Admin"
                }
            };

            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetAllAsync()).ReturnsAsync(outcomes);

            //Act
            var outcomesDto = await Service.GetAllAsync();

            //Assert
            Assert.Equal(outcomes.Count, outcomesDto.Count);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            //Arrange
            var outcome = new Domain.Entity.Outcome()
            {
                Id = Guid.NewGuid(),
                Count = 10,
                ProductId = Guid.NewGuid(),
                ProductName = "Test",
                OutcomeDate = DateTime.Now,
                Recipient = "Test"
            };
            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository.GetByIdAsync(outcome.Id)).ReturnsAsync(outcome);

            //Act
            var outcomeDto = await Service.GetByIdAsync(outcome.Id);

            //Assert
            Assert.Equal(outcome.Id, outcomeDto.Id);
        }

        [Fact]
        public async Task GetByIdAsync_FailOnWrongId()
        {
            //Arrange
            var wrongId = Guid.NewGuid();
            UnitOfWorkMock.Setup(x => x.OutcomeRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            //Assert
            await Assert.ThrowsAsync<OutcomeNotFoundException>(async () => await Service.GetByIdAsync(wrongId));

        }
    }
}
