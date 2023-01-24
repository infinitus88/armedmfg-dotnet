using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class ProductTypeService : IProductTypeService
{
    private readonly IProductsLookupDataService<ProductCategory> _categoryService;
    private readonly HttpService _httpService;
    private readonly ILogger<CatalogItemService> _logger;

    public ProductTypeService(IProductsLookupDataService<ProductCategory> categoryService, HttpService httpService, ILogger<CatalogItemService> logger)
    {
        _categoryService = categoryService;
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<ProductType> Create(CreateProductTypeRequest productType)
    {
        var response = await _httpService.HttpPost<CreateProductTypeResponse>("product-types", productType);
        return response?.ProductType;
    }

    public async Task<ProductType> Edit(ProductType productType)
    {
        return (await _httpService.HttpPut<EditProductTypeResult>("product-types", productType)).ProductType;
    }

    public async Task<string> Delete(int productTypeId)
    {
        return (await _httpService.HttpDelete<DeleteCatalogItemResponse>("product-types", productTypeId)).Status;
    }

    public async Task<ProductType> GetById(int id)
    {
        var categoryListTask = _categoryService.List();
        var productTypeGetTask = _httpService.HttpGet<EditProductTypeResult>($"product-types/{id}");
        await Task.WhenAll(categoryListTask, productTypeGetTask);
        var categories = categoryListTask.Result;
        var productType = productTypeGetTask.Result.ProductType;
        productType.ProductCategory = categories.FirstOrDefault(t => t.Id == productType.ProductCategoryId)?.Name;
        return productType;    
    }

    public async Task<List<ProductType>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching product types from API.");

        var categoryListTask = _categoryService.List();
        var productTypeListTask = _httpService.HttpGet<PagedProductTypeResponse>($"product-types?PageSize=10");
        await Task.WhenAll(categoryListTask, productTypeListTask);
        var categories = categoryListTask.Result;
        var productTypes = productTypeListTask.Result.ProductTypes;
        foreach (var productType in productTypes)
        {
            productType.ProductCategory = categories.FirstOrDefault(t => t.Id == productType.ProductCategoryId)?.Name;
        }
        return productTypes;    
    }

    public async Task<List<ProductType>> List()
    {
        _logger.LogInformation("Fetching product types from API.");

        var categoryListTask = _categoryService.List();
        var productTypeListTask = _httpService.HttpGet<PagedProductTypeResponse>($"product-types");
        await Task.WhenAll(categoryListTask, productTypeListTask);
        var categories = categoryListTask.Result;
        var productTypes = productTypeListTask.Result.ProductTypes;
        foreach (var productType in productTypes)
        {
            productType.ProductCategory = categories.FirstOrDefault(t => t.Id == productType.ProductCategoryId)?.Name;
        }
        return productTypes;    
    }
}
