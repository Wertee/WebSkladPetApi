using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;
using Application.Product.DTO;
using AutoMapper;

namespace Application.Common.Mapping
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<ProductDTO, Domain.Entity.Product>().ReverseMap();
            CreateMap<CategoryDTO, Domain.Entity.Category>().ReverseMap();
        }
    }
}
