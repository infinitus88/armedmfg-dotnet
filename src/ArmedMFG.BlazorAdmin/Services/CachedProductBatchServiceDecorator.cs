using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedProductBatchServiceDecorator : IProductBatchService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ProductBatchService _productBatchService;
    private ILogger<CachedProductBatchServiceDecorator> _logger;

    public CachedProductBatchServiceDecorator(ILocalStorageService localStorageService, ProductBatchService productBatchService, ILogger<CachedProductBatchServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _productBatchService = productBatchService;
        _logger = logger;
    }

    public async Task<ProductBatch> Create(CreateProductBatchRequest productBatch)
    {
        var result = await _productBatchService.Create(productBatch);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<ProductBatch> Edit(ProductBatch productBatch)
    {
        var result = await _productBatchService.Edit(productBatch);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _productBatchService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<ProductBatch> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<ProductBatch>> ListPaged(int pageSize)
    {
        string key = "productBatches";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<ProductBatch>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading product batches from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.Now)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var productBatches = await _productBatchService.ListPaged(pageSize);
        var entry = new CacheEntry<List<ProductBatch>>(productBatches);
        await _localStorageService.SetItemAsync(key, entry);
        return productBatches;
    }

    public async Task<List<ProductBatch>> List()
    {
        string key = "productBatches";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<ProductBatch>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading product batches from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var productBatches = await _productBatchService.List();
        var entry = new CacheEntry<List<ProductBatch>>(productBatches);
        await _localStorageService.SetItemAsync(key, entry);
        return productBatches;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "productBatches";

        await _localStorageService.RemoveItemAsync(key);
        var productBatches = await _productBatchService.List();
        var entry = new CacheEntry<List<ProductBatch>>(productBatches);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
