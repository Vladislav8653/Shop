using AutoMapper;
using ProductManagement.Application.DTO;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductRequestDto, Product>()
            .ForMember(p => p.CreatedAt, 
                opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<Product, ProductRequestDto>();
    }
}