using AutoMapper;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.PublicApi.CatalogBrandEndpoints;
using ArmedMFG.PublicApi.CatalogItemEndpoints;
using ArmedMFG.PublicApi.CatalogTypeEndpoints;
using ArmedMFG.PublicApi.CustomerEndpoints;
using ArmedMFG.PublicApi.CustomerOrganizationEndpoints;
using ArmedMFG.PublicApi.MaterialCategoryEndpoints;
using ArmedMFG.PublicApi.MaterialTypeEndpoints;
using ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;
using ArmedMFG.PublicApi.OrderEndpoints;
using ArmedMFG.PublicApi.ProductBatchEndpoints;
using ArmedMFG.PublicApi.ProductCategoryEndpoints;
using ArmedMFG.PublicApi.ProductTypeEndpoints;
using ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;
using Microsoft.CodeAnalysis.Options;

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

        // TODO Remove null checks for GetCurrentPrice()
        CreateMap<ProductType, ProductTypeDto>()
            .ForMember(dto => dto.CurrentPrice, 
                options => options.MapFrom(src => src.GetCurrentPrice() == null ? 0 : src.GetCurrentPrice()));
        CreateMap<ProductCategory, ProductCategoryDto>();

        CreateMap<ProductPrice, ProductPriceDto>();

        CreateMap<MaterialType, MaterialTypeDto>();
        CreateMap<MaterialCategory, MaterialCategoryDto>();

        CreateMap<MaterialSupply, MaterialSupplyDto>();

        CreateMap<ProductBatch, ProductBatchDto>();
        CreateMap<ProducedProduct, ProducedProductDto>();
        CreateMap<SpentMaterial, SpentMaterialDto>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerOrganization, CustomerOrganizationDto>()
            .ForMember(dto => dto.MainBranchAddress,
                options => options.MapFrom(src => src.MainBranchAddress.ToString()));

        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderInfoDto>()
            .ForMember(dto => dto.CustomerFullName,
                options => options.MapFrom(src => src.Customer.FullName));
        CreateMap<OrderShipment, OrderShipmentDto>();
        CreateMap<OrderProduct, OrderProductDto>();
        CreateMap<OrderProduct, OrderProductInfoDto>()
            .ForMember(dto => dto.ProductTypeName,
                options => options.MapFrom(src => src.ProductType.Name));

        CreateMap<ShipmentProduct, ShipmentProductDto>();
    }
}
