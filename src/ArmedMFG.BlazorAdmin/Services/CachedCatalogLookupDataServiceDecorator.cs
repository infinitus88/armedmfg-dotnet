using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedCatalogLookupDataServiceDecorator<TLookupData, TResponse>
    : ICatalogLookupDataService<TLookupData>
    where TLookupData : LookupData
    where TResponse : ILookupDataResponse<TLookupData>
{
    private readonly ILocalStorageService _localStorageService;
    private readonly CatalogLookupDataService<TLookupData, TResponse> _catalogTypeService;
    private ILogger<CachedCatalogLookupDataServiceDecorator<TLookupData, TResponse>> _logger;

    public CachedCatalogLookupDataServiceDecorator(ILocalStorageService localStorageService,
        CatalogLookupDataService<TLookupData, TResponse> catalogTypeService,
        ILogger<CachedCatalogLookupDataServiceDecorator<TLookupData, TResponse>> logger)
    {
        _localStorageService = localStorageService;
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    public async Task<List<TLookupData>> List()
    {
        string key = typeof(TLookupData).Name;
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<TLookupData>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation($"Loading {key} from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Cache expired; removing {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var types = await _catalogTypeService.List();
        var entry = new CacheEntry<List<TLookupData>>(types);
        await _localStorageService.SetItemAsync(key, entry);
        return types;
    }
}
