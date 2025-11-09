using AutoMapper;
using ECommerce.DTOs;
using ECommerce.Models;

namespace ECommerce.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FilterBy, FilterDto>();
            CreateMap<FilterDto, FilterBy>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

        }
        
    }
}
