using Application.Category.Services;
using Application.Common.Mapping;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Category
{
    public class CategoryServiceCreator
    {
        public static CategoryService CreateService(WebSkladDbContext context)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            var mockMapper = mapperConfiguration.CreateMapper();
            var unitOfWork = new UnitOfWork(context);
            var service = new CategoryService(unitOfWork, mockMapper);

            return service;
        }
    }
}
