using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class ProductPriceService : IProductPriceService
{
    // private readonly IProductsLookupDataService<ProductType> _productTypeService;
    private readonly HttpService _httpService;
    private readonly ILogger<ProductPrice> _logger;

    public ProductPriceService(HttpService httpService, ILogger<ProductPrice> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }


    public async Task<ProductPrice> Create(CreateProductPriceRequest productPrice)
    {
        var response = await _httpService.HttpPost<CreateProductPriceResponse>("product-types/prices", productPrice);
        return response?.ProductPrice;
    }

    public async Task<ProductPrice> Edit(ProductPrice productPrice)
    {
        return (await _httpService.HttpPut<EditProductPriceResult>("product-types/prices", productPrice)).ProductPrice;
    }

    public async Task<string> Delete(int productPriceId)
    {
        return (await _httpService.HttpDelete<DeleteProductPriceResponse>("product-types/prices", productPriceId)).Status;
    }

    public async Task<ProductPrice> GetById(int id)
    {
        // var productTypeListTask = _productTypeService.List();
        var productPriceGetTask = _httpService.HttpGet<EditProductPriceResult>($"product-types/prices/{id}");
        // await Task.WhenAll(productTypeListTask,productPriceGetTask);
        await Task.WhenAll(productPriceGetTask);
        // var productTypes = productTypeListTask.Result;
        var productPrice = productPriceGetTask.Result.ProductPrice;
        // productPrice.ProductType = productTypes.FirstOrDefault(t => t.Id == productPrice.ProductTypeId)?.Name;
        return productPrice;
    }

    public async Task<List<ProductPrice>> ListPaged(int pageSize, int productTypeId)
    {
        _logger.LogInformation("Fetching product prices from API.");

        // var productTypeListTask = _productTypeService.List();
        var productPriceListTask = _httpService.HttpGet<PagedProductPriceResponse>($"product-types/prices?PageSize=10&ProductTypeId={productTypeId}");
        await Task.WhenAll(productPriceListTask);
        // var productTypes = productTypeListTask.Result;
        var productPrices = productPriceListTask.Result.ProductPrices;

        // foreach (var productPrice in productPrices)
        // {
        //     productPrice.ProductType = productTypes.FirstOrDefault(t => t.Id == productPrice.ProductTypeId)?.Name;
        // }

        return productPrices;
    }

    public async Task<List<ProductPrice>> List()
    {
        _logger.LogInformation("Fetching product prices from API.");

        // var productTypeListTask = _productTypeService.List();
        var productPriceListTask = _httpService.HttpGet<PagedProductPriceResponse>($"product-types/prices");
        // await Task.WhenAll(productTypeListTask, productPriceListTask);
        await Task.WhenAll(productPriceListTask);
        // var productTypes = productTypeListTask.Result;
        var productPrices = productPriceListTask.Result.ProductPrices;

        // foreach (var productPrice in productPrices)
        // {
        //     productPrice.ProductType = productTypes.FirstOrDefault(t => t.Id == productPrice.ProductTypeId)?.Name;
        // }

        return productPrices;
    }
}
