using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedProductTypeServiceDecorator : IProductTypeService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ProductTypeService _productTypeService;
    private ILogger<CachedProductTypeServiceDecorator> _logger;

    public CachedProductTypeServiceDecorator(ILocalStorageService localStorageService, ProductTypeService productTypeService, ILogger<CachedProductTypeServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _productTypeService = productTypeService;
        _logger = logger;
    }

    public async Task<ProductType> Create(CreateProductTypeRequest productType)
    {
        var result = await _productTypeService.Create(productType);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<ProductType> Edit(ProductType productType)
    {
        var result = await _productTypeService.Edit(productType);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _productTypeService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<ProductType> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<ProductType>> ListPaged(int pageSize)
    {
        string key = "productTypes";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<ProductType>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading product types from local storage.");
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

        var productTypes = await _productTypeService.ListPaged(pageSize);
        var entry = new CacheEntry<List<ProductType>>(productTypes);
        await _localStorageService.SetItemAsync(key, entry);
        return productTypes;
    }

    public async Task<List<ProductType>> List()
    {
        string key = "productTypes";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<ProductType>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading product types from local storage.");
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

        var productTypes = await _productTypeService.List();
        var entry = new CacheEntry<List<ProductType>>(productTypes);
        await _localStorageService.SetItemAsync(key, entry);
        return productTypes;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "productTypes";

        await _localStorageService.RemoveItemAsync(key);
        var productTypes = await _productTypeService.List();
        var entry = new CacheEntry<List<ProductType>>(productTypes);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
