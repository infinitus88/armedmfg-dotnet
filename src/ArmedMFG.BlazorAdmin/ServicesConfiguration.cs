using ArmedMFG.BlazorAdmin.Services;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ArmedMFG.BlazorAdmin;

public static class ServicesConfiguration
{
    public static IServiceCollection AddBlazorServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogLookupDataService<CatalogBrand>, CachedCatalogLookupDataServiceDecorator<CatalogBrand, CatalogBrandResponse>>();
        services.AddScoped<CatalogLookupDataService<CatalogBrand, CatalogBrandResponse>>();
        services.AddScoped<ICatalogLookupDataService<CatalogType>, CachedCatalogLookupDataServiceDecorator<CatalogType, CatalogTypeResponse>>();
        services.AddScoped<CatalogLookupDataService<CatalogType, CatalogTypeResponse>>();
        services.AddScoped<ICatalogItemService, CachedCatalogItemServiceDecorator>();
        services.AddScoped<CatalogItemService>();

        services.AddScoped<IProductsLookupDataService<ProductCategory>,
                CachedProductsLookupDataServiceDecorator<ProductCategory, ProductCategoryResponse>>();
        services.AddScoped<ProductsLookupDataService<ProductCategory, ProductCategoryResponse>>();
        services.AddScoped<IProductTypeService, CachedProductTypeServiceDecorator>();
        services.AddScoped<ProductTypeService>();

        services.AddScoped<IProductsLookupDataService<MaterialCategory>,
                CachedProductsLookupDataServiceDecorator<MaterialCategory, MaterialCategoryResponse>>();
        services.AddScoped<ProductsLookupDataService<MaterialCategory, MaterialCategoryResponse>>();
        services.AddScoped<IMaterialTypeService, CachedMaterialTypeServiceDecorator>();
        services.AddScoped<MaterialTypeService>();
        
        return services;
    }
}
