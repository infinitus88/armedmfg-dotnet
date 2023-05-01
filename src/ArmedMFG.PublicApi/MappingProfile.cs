using AutoMapper;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.PublicApi.Configuration;
using ArmedMFG.PublicApi.CustomerEndpoints;
using ArmedMFG.PublicApi.CustomerOrganizationEndpoints;
using ArmedMFG.PublicApi.MaterialCategoryEndpoints;
using ArmedMFG.PublicApi.MaterialTypeEndpoints;
using ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;
using ArmedMFG.PublicApi.OrderEndpoints;
using ArmedMFG.PublicApi.PaymentRecordEndpoints;
using ArmedMFG.PublicApi.ProductBatchEndpoints;
using ArmedMFG.PublicApi.ProductCategoryEndpoints;
using ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;
using ArmedMFG.PublicApi.ProductTypeEndpoints;
using ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using ArmedMFG.PublicApi.EmployeeEndpoints;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;

namespace ArmedMFG.PublicApi;

public class MappingProfile : Profile
{
    private readonly DateParsingSettings _dateParsingSettings;
    
    public MappingProfile(IOptions<DateParsingSettings> dateParsingSettings)
    {
        _dateParsingSettings = dateParsingSettings.Value;
        
        // CreateMap<CatalogItem, CatalogItemDto>();
        // CreateMap<CatalogType, CatalogTypeDto>()
        //     .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Type));
        // CreateMap<CatalogBrand, CatalogBrandDto>()
        //     .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Brand));

        // TODO Remove null checks for GetCurrentPrice()
        CreateMap<ProductType, ProductTypeDto>()
            .ForMember(dto => dto.CurrentPrice, 
                options => options.MapFrom(src => src.GetCurrentPrice() == null ? 0 : src.GetCurrentPrice()));
        CreateMap<ProductType, ProductTypeInfoDto>()
            .ForMember(dto => dto.CurrentPrice, 
                options => options.MapFrom(src => src.GetCurrentPrice() == null ? 0 : src.GetCurrentPrice()));
        CreateMap<ProductCategory, ProductCategoryDto>();

        CreateMap<ProductPrice, ProductPriceDto>();

        CreateMap<ProductCheckPoint, ProductCheckPointDto>()
            .ForMember(dto => dto.ProductName,
                options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(dto => dto.ProductCategoryId,
                options => options.MapFrom(src => src.ProductType.ProductCategoryId));

        CreateMap<MaterialType, MaterialTypeDto>();
        // CreateMap<MaterialType, MaterialTypeInfoDto>();
        CreateMap<MaterialCategory, MaterialCategoryDto>();

        CreateMap<MaterialSupply, MaterialSupplyDto>();

        CreateMap<ProductBatch, ProductBatchDto>();
        CreateMap<ProductBatch, ProductBatchInfoDto>()
            .ForMember(dst => dst.ProducedDate,
            options => options.MapFrom(src => src.ProducedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)));
        CreateMap<ProducedProduct, ProducedProductDto>();
        CreateMap<SpentMaterial, SpentMaterialDto>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerInfoDto>();

        CreateMap<CustomerOrganization, CustomerOrganizationDto>()
            .ForMember(dto => dto.MainBranchAddress,
                options => options.MapFrom(src => src.MainBranchAddress.ToString()));
        CreateMap<CustomerOrganization, OrganizationInfoDto>()
            .ForMember(dto => dto.MainBranchAddress,
                options => options.MapFrom(src => src.MainBranchAddress.ToString()));
        
        

        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderDetailDto>()
            .ForMember(dto => dto.CustomerName,
                options => options.MapFrom(src => src.Customer!.FullName))
            .ForMember(dto => dto.OrderedDate,
                options => options.MapFrom(src => src.OrderedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.RequiredDate,
                options => options.MapFrom(src => src.RequiredDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.FinishedDate,
                options => options.MapFrom(src =>
                    src.FinishedDate!.Value.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.OrderProducts,
                options => options.Ignore())
            .ForMember(dto => dto.OrderShipments,
                options => options.Ignore())
            .ForMember(dto => dto.OrderPaymentRecords,
                options => options.Ignore());
        
        CreateMap<Order, OrderSearchDto>()
            .ForMember(dto => dto.CustomerFullName,
                options => options.MapFrom(src => src.Customer!.FullName))
            .ForMember(dto => dto.OrderedDate,
                options => options.MapFrom(src => src.OrderedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.RequiredDate,
                options => options.MapFrom(src => src.RequiredDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.FinishedDate,
                options => options.MapFrom(src => src.FinishedDate!.Value.ToString(_dateParsingSettings.DefaultDisplayDateFormat)));
        
        CreateMap<OrderShipment, OrderShipmentDto>()
            .ForMember(dto => dto.ShipmentDate,
                options => options.MapFrom(src => src.ShipmentDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)));
        CreateMap<OrderShipment, OrderShipmentInfoDto>()
            .ForMember(dto => dto.CustomerFullName,
                options => options.MapFrom(src => src.Order.Customer.FullName));
        CreateMap<OrderProduct, OrderProductDto>()
            .ForMember(dst => dst.ProductTypeName, 
                options => options.MapFrom(src => src.ProductType.Name));
        CreateMap<OrderProduct, OrderProductInfoDto>()
            .ForMember(dto => dto.ProductTypeName,
                options => options.MapFrom(src => src.ProductType.Name));
        CreateMap<PaymentRecord, OrderPaymentRecordDto>()
            .ForMember(dto => dto.PayedDate,
                options => options.MapFrom(src => src.PayedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.PaymentMethod,
                options => options.MapFrom(src => (byte)src.PaymentMethod));

        // Employees
        CreateMap<Employee, EmployeeListItemDto>()
            .ForMember(dto => dto.DateOfBirth,
                options => options.MapFrom(src => src.DateOfBirth.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.JoiningDate,
                options => options.MapFrom(src => src.JoiningDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)));

        CreateMap<PaymentRecord, PaymentRecordDto>()
            .ForMember(dto => dto.PayedDate,
                options => options.MapFrom(src => src.PayedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.PaymentMethod,
                options => options.MapFrom(src => (byte)src.PaymentMethod))
            .ForMember(dto => dto.Type,
                options => options.MapFrom(src => (byte)src.PaymentCategory!.Type));

        CreateMap<ShipmentProduct, ShipmentProductDto>();
    }
}
