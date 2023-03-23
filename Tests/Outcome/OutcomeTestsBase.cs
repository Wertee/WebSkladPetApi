using Application.Interfaces;
using Domain.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mapping;
using Application.Outcome.Services;
using AutoMapper;

namespace Tests.Outcome
{
    public class OutcomeTestsBase
    {
        protected OutcomeService Service;
        protected readonly Mock<IUnitOfWork> UnitOfWorkMock = new();
        protected readonly Mock<IRepository<Domain.Entity.Category>> CategoryRepoMock = new();
        protected readonly Mock<IRepository<Product>> ProductRepoMock = new();
        protected readonly Mock<IRepository<Domain.Entity.Outcome>> OutcomeRepoMock = new();
        protected readonly IMapper Mapper;
        public OutcomeTestsBase()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            Mapper = mapperConfiguration.CreateMapper();

            UnitOfWorkMock.Setup(uow => uow.OutcomeRepository).Returns(OutcomeRepoMock.Object);
            UnitOfWorkMock.Setup(uow => uow.CategoryRepository).Returns(CategoryRepoMock.Object);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository).Returns(ProductRepoMock.Object);
            UnitOfWorkMock.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask);

            Service = new OutcomeService(UnitOfWorkMock.Object, Mapper);
        }
    }
}
