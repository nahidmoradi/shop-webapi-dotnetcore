using AutoMapper;
using Shop.Core.Domain.Entities;
using Shop.Application.DTOs;

namespace Shop.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<ProductDto, Product>();

        // Category mappings
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
    }
}