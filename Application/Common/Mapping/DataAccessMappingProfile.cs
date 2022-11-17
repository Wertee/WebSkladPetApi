using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;
using Application.Outcome.DTO;
using Application.Product.DTO;
using AutoMapper;

namespace Application.Common.Mapping
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Domain.Entity.Product, ProductDTO>().ForMember(categoryName => categoryName.CategoryName,
                opt => opt.MapFrom(product => product.Category.Name));

            CreateMap<ProductDTO, Domain.Entity.Product>()
                .ForSourceMember(x => x.CategoryName, opt => opt.DoNotValidate());

            CreateMap<CategoryDTO, Domain.Entity.Category>().ReverseMap();

            CreateMap<Domain.Entity.Outcome, OutcomeDTO>().ReverseMap();

        }
    }
}
