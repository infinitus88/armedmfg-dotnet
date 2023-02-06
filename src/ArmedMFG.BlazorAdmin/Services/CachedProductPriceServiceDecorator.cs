using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedProductPriceServiceDecorator : IProductPriceService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ProductPriceService _productPriceService;
    private readonly ILogger<CachedProductPriceServiceDecorator> _logger;

    public CachedProductPriceServiceDecorator(ILocalStorageService localStorageService, ProductPriceService productPriceService, ILogger<CachedProductPriceServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _productPriceService = productPriceService;
        _logger = logger;
    }


    public async Task<ProductPrice> Create(CreateProductPriceRequest productPrice)
    {
        var result = await _productPriceService.Create(productPrice);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<ProductPrice> Edit(ProductPrice productPrice)
    {
        var result = await _productPriceService.Edit(productPrice);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _productPriceService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<ProductPrice> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<ProductPrice>> ListPaged(int pageSize, int? productTypeId)
    {
        string key = "productPrices";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<ProductPrice>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading product prices from local storage.");
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

        var productPrices = await _productPriceService.ListPaged(pageSize, productTypeId);
        var entry = new CacheEntry<List<ProductPrice>>(productPrices);
        await _localStorageService.SetItemAsync(key, entry);
        return productPrices;
    }

    public async Task<List<ProductPrice>> List()
    {
        string key = "productPrices";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<ProductPrice>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading product prices from local storage.");
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

        var productPrices = await _productPriceService.List();
        var entry = new CacheEntry<List<ProductPrice>>(productPrices);
        await _localStorageService.SetItemAsync(key, entry);
        return productPrices;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "productPrices";

        await _localStorageService.RemoveItemAsync(key);
        var productPrices = await _productPriceService.List();
        var entry = new CacheEntry<List<ProductPrice>>(productPrices);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
