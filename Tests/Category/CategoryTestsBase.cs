using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.Services;
using Application.Common.Mapping;
using Application.Interfaces;
using AutoMapper;
using Moq;

namespace Tests.Category
{
    public abstract class CategoryTestsBase
    {
        protected readonly CategoryService _service;
        protected readonly IMapper _mapper;
        protected readonly Mock<ICategoryRepository> _categoryRepositoryMock = new Mock<ICategoryRepository>();

        protected CategoryTestsBase()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<DataAccessMappingProfile>(); });
            _mapper = mapperConfiguration.CreateMapper();
            _service = new CategoryService(_categoryRepositoryMock.Object, _mapper);
        }
    }
}
