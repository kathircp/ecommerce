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
            CreateMap<UserDetailDto, UserDetail>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ReverseMap();
            CreateMap<OrderCreateDto, OrderDto>().ReverseMap();
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<PaymentDto, Payment>().ReverseMap();
            CreateMap<FileStorageDto, FileStorage>().ReverseMap();
        }
        
    }
}
