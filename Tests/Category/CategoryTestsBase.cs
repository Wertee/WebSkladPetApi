using Application.Category.Services;
using Application.Common.Mapping;
using Application.Interfaces;
using Application.Product.Services;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Category
{
    public abstract class CategoryTestsBase
    {
        protected readonly CategoryService _service;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        protected readonly Mock<IRepository<Domain.Entity.Category>> CategoryRepoMock = new();
        protected readonly IMapper _mapper;
        protected CategoryTestsBase()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            _mapper = mapperConfiguration.CreateMapper();

            var options =
            new DbContextOptionsBuilder<WebSkladDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new WebSkladDbContext(options);
            context.Database.EnsureCreated();

            _unitOfWorkMock.Setup(uow => uow.CategoryRepository).Returns(CategoryRepoMock.Object);

            _service = new CategoryService(_unitOfWorkMock.Object, _mapper);
        }
    }
}
