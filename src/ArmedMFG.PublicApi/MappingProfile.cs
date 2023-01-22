using AutoMapper;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.PublicApi.CatalogBrandEndpoints;
using ArmedMFG.PublicApi.CatalogItemEndpoints;
using ArmedMFG.PublicApi.CatalogTypeEndpoints;

namespace ArmedMFG.PublicApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDto>();
        CreateMap<CatalogType, CatalogTypeDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Type));
        CreateMap<CatalogBrand, CatalogBrandDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Brand));
    }
}
