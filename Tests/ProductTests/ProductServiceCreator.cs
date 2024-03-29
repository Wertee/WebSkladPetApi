﻿using Application.Product.Services;
using AutoMapper;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mapping;
using Infrastructure;

namespace Tests.ProductTests
{
    public class ProductServiceCreator
    {
        public static ProductService CreateService(WebSkladDbContext context)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            var mockMapper = mapperConfiguration.CreateMapper();
            var unitOfWork = new UnitOfWork(context);
            var service = new ProductService(mockMapper, unitOfWork);
            return service;
        }
    }
}
