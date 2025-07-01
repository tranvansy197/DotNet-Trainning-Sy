using App.Api.Domains;
using App.Api.Models;
using AutoMapper;

namespace App.Api.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<ProductCreatedDTO, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore());
        CreateMap<RegisterRequestDTO, User>();
    }
}