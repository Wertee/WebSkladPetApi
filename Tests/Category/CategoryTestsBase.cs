using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.Services;
using Application.Common.Mapping;
using Application.Interfaces;
using Application.Product.Services;
using AutoMapper;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tests.Common;

namespace Tests.Category
{
    public abstract class CategoryTestsBase : TestServicesBase
    {
        protected readonly CategoryService Service;
        protected readonly IMapper Mapper;
        protected CategoryTestsBase()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            Mapper = mapperConfiguration.CreateMapper();

            Service = CategoryServiceCreator.CreateService(Context);
        }
    }
}
