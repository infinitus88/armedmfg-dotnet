using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedProductsLookupDataServiceDecorator<TLookupData, TResponse>
    : IProductsLookupDataService<TLookupData>
    where TLookupData : LookupData
    where TResponse : ILookupDataResponse<TLookupData>
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ProductsLookupDataService<TLookupData, TResponse> _categoryService;
    private ILogger<CachedProductsLookupDataServiceDecorator<TLookupData, TResponse>> _logger;

    public CachedProductsLookupDataServiceDecorator(ILocalStorageService localStorageService,
        ProductsLookupDataService<TLookupData, TResponse> categoryService,
        ILogger<CachedProductsLookupDataServiceDecorator<TLookupData, TResponse>> logger)
    {
        _localStorageService = localStorageService;
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<List<TLookupData>> List()
    {
        string key = typeof(TLookupData).Name;
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<TLookupData>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation($"Loading {key} from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.Now)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Cache expired; removing {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var categories = await _categoryService.List();
        var entry = new CacheEntry<List<TLookupData>>(categories);
        await _localStorageService.SetItemAsync(key, entry);
        return categories;
    }
}
