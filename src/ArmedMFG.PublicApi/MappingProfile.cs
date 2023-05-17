using AutoMapper;
using ArmedMFG.ApplicationCore.Entities.CustomerAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.PublicApi.Configuration;
using Microsoft.Extensions.Options;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;
using ArmedMFG.PublicApi.Modules.MaterialCategories;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.ProductCategoryEndpoints;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;

using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;
using ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;
using ArmedMFG.PublicApi.Modules.OrderShipments.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi;

public class MappingProfile : Profile
{
    private readonly DateParsingSettings _dateParsingSettings;
    
    public MappingProfile(IOptions<DateParsingSettings> dateParsingSettings)
    {
        _dateParsingSettings = dateParsingSettings.Value;

        CustomersMapping();
        EmployeesMapping();
        OrdersMapping();
        PaymentRecordsMapping();
        ProductionReportsMapping();
        
    }
    
    // Customers
    private void CustomersMapping()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerForEditDto>();
    }

    // Materials
    private void MaterialsMapping()
    {
        CreateMap<Material, MaterialDto>();
        CreateMap<MaterialCategory, MaterialCategoryDto>();
        CreateMap<MaterialSupply, MaterialSupplyDto>();
    }

    // Orders
    private void OrdersMapping()
    {

        CreateMap<Order, OrderDto>()
            .ForMember(dto => dto.OrderedDate,
                options => options.MapFrom(src => src.OrderedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.RequiredDate,
                options => options.MapFrom(src => src.RequiredDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.FinishedDate,
                options => options.MapFrom(src => src.FinishedDate!.Value.ToString(_dateParsingSettings.DefaultDisplayDateFormat)));

        CreateMap<PaymentRecord, OrderPaymentRecordDto>()
            .ForMember(dto => dto.PayedDate,
                options => options.MapFrom(src => src.PayedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.PaymentMethod,
                options => options.MapFrom(src => (byte)src.PaymentMethod));

        CreateMap<OrderShipment, OrderShipmentDto>()
           .ForMember(dto => dto.ShipmentDate,
               options => options.MapFrom(src => src.ShipmentDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)));
        CreateMap<OrderProduct, OrderProductForOrderDto>()
            .ForMember(dst => dst.ProductTypeName,
                options => options.MapFrom(src => src.ProductType.Name));
        CreateMap<OrderProduct, OrderProductInfoDto>()
            .ForMember(dto => dto.ProductTypeName,
                options => options.MapFrom(src => src.ProductType.Name));

    }

    // Products
    private void ProductsMapping()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductForEditDto>();
        CreateMap<ProductCategory, ProductCategoryDto>();
    }

    // PaymentRecords
    private void PaymentRecordsMapping()
    {
        CreateMap<PaymentRecord, PaymentRecordDto>()
            .ForMember(dto => dto.PayedDate,
                options => options.MapFrom(src => src.PayedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.PaymentMethod,
                options => options.MapFrom(src => (byte)src.PaymentMethod))
            .ForMember(dto => dto.Type,
                options => options.MapFrom(src => (byte)src.PaymentCategory!.Type));

        CreateMap<PaymentRecord, OrderPaymentRecordDto>()
            .ForMember(dto => dto.PayedDate,
                options => options.MapFrom(src => src.PayedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)))
            .ForMember(dto => dto.PaymentMethod,
                options => options.MapFrom(src => (byte)src.PaymentMethod));
    }

    // ProductionReports
    private void ProductionReportsMapping()
    {
        //    CreateMap<ProductionReport, ProductBatchDto>();
        //    CreateMap<ProductionReport, ProductBatchInfoDto>()
        //        .ForMember(dst => dst.ProducedDate,
        //        options => options.MapFrom(src => src.ProducedDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat)));
        //    CreateMap<ProducedProduct, ProducedProductDto>();
    }

    // Employees
    private void EmployeesMapping()
    {
    }
}
