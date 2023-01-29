using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class ProductBatchService : IProductBatchService
{
    private readonly IProductTypeService _productTypeService;
    private readonly IMaterialTypeService _materialTypeService;
    
    private readonly HttpService _httpService;
    private readonly ILogger<ProductBatch> _logger;

    public ProductBatchService(HttpService httpService, ILogger<ProductBatch> logger, IProductTypeService productTypeService, IMaterialTypeService materialTypeService)
    {
        _httpService = httpService;
        _logger = logger;
        _productTypeService = productTypeService;
        _materialTypeService = materialTypeService;
    }
    
    public async Task<ProductBatch> Create(CreateProductBatchRequest productBatch)
    {
        var response = await _httpService.HttpPost<CreateProductBatchResponse>("product-batch", productBatch);
        return response?.ProductBatch;
    }

    public async Task<ProductBatch> Edit(ProductBatch productBatch)
    {
        return (await _httpService.HttpPut<EditProductBatchResult>("product-batch", productBatch)).ProductBatch;
    }

    public async Task<string> Delete(int id)
    {
        return (await _httpService.HttpDelete<DeleteProductBatchResponse>("product-batch", id)).Status;
    }

    public async Task<ProductBatch> GetById(int id)
    {
        var productTypeListTask = _productTypeService.List();
        var materialTypeListTask = _materialTypeService.List();
        var productBatchGetTask = _httpService.HttpGet<EditProductBatchResult>($"product-batch/{id}");
        await Task.WhenAll(productTypeListTask, materialTypeListTask, productBatchGetTask);
        var productTypes = productTypeListTask.Result;
        var materialTypes = materialTypeListTask.Result;
        var productBatch = productBatchGetTask.Result.ProductBatch;
        productBatch.ProducedProducts.ForEach(p =>
            p.ProductType = productTypes.FirstOrDefault(t => t.Id == p.ProductTypeId)?.Name);
        productBatch.SpentMaterials.ForEach(m => 
            m.MaterialType = materialTypes.FirstOrDefault(t => t.Id == m.MaterialTypeId)?.Name);
        return productBatch;
    }

    public async Task<List<ProductBatch>> ListPaged(int pageSize = 10)
    {
        _logger.LogInformation("Fetching product batches from API.");

        var productTypeListTask = _productTypeService.List();
        var materialTypeListTask = _materialTypeService.List();
        var productBatchListTask = _httpService.HttpGet<PagedProductBatchResponse>($"product-batches?PageSize={pageSize}");
        await Task.WhenAll(productBatchListTask, materialTypeListTask, productBatchListTask);

        var productTypes = productTypeListTask.Result;
        var materialTypes = materialTypeListTask.Result;
        var productBatches = productBatchListTask.Result.ProductBatches;

        foreach (var productBatch in productBatches)
        {
            productBatch.ProducedProducts.ForEach(p => p.ProductType = productTypes.FirstOrDefault(t => t.Id == p.ProductTypeId)?.Name);
            productBatch.SpentMaterials.ForEach(m => m.MaterialType = materialTypes.FirstOrDefault(t => t.Id == m.MaterialTypeId)?.Name);
        }

        return productBatches;
    }

    public async Task<List<ProductBatch>> List()
    {
        _logger.LogInformation("Fetching product batches from API.");

        var productTypeListTask = _productTypeService.List();
        var materialTypeListTask = _materialTypeService.List();
        var productBatchListTask = _httpService.HttpGet<PagedProductBatchResponse>($"product-batches");
        await Task.WhenAll(productTypeListTask, materialTypeListTask, productBatchListTask);

        var productTypes = productTypeListTask.Result;
        var materialTypes = materialTypeListTask.Result;
        var productBatches = productBatchListTask.Result.ProductBatches;

        foreach (var productBatch in productBatches)
        {
            productBatch.ProducedProducts.ForEach(p => p.ProductType = productTypes.FirstOrDefault(t => t.Id == p.ProductTypeId)?.Name);
            productBatch.SpentMaterials.ForEach(m => m.MaterialType = materialTypes.FirstOrDefault(t => t.Id == m.MaterialTypeId)?.Name);
        }

        return productBatches;
    }
}
