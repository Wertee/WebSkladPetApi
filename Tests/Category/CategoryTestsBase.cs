using Application.Category.Services;
using Application.Common.Mapping;
using Application.Interfaces;
using Application.Product.Services;
using AutoMapper;
using Domain.Entity;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Category
{
    public abstract class CategoryTestsBase
    {
        protected readonly CategoryService Service;
        protected readonly Mock<IUnitOfWork> UnitOfWorkMock = new Mock<IUnitOfWork>();
        protected readonly Mock<IRepository<Domain.Entity.Category>> CategoryRepoMock = new();
        protected readonly Mock<IRepository<Product>> ProductRepoMock = new();
        protected readonly IMapper Mapper;
        protected CategoryTestsBase()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            Mapper = mapperConfiguration.CreateMapper();

            var options =
            new DbContextOptionsBuilder<WebSkladDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new WebSkladDbContext(options);
            context.Database.EnsureCreated();

            UnitOfWorkMock.Setup(uow => uow.CategoryRepository).Returns(CategoryRepoMock.Object);
            UnitOfWorkMock.Setup(uow => uow.ProductRepository).Returns(ProductRepoMock.Object);
            UnitOfWorkMock.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask);

            Service = new CategoryService(UnitOfWorkMock.Object, Mapper);
        }
    }
}
