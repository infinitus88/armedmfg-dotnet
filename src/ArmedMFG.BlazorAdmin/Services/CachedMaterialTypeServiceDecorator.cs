using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedMaterialTypeServiceDecorator : IMaterialTypeService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly MaterialTypeService _materialTypeService;
    private ILogger<CachedMaterialTypeServiceDecorator> _logger;

    public CachedMaterialTypeServiceDecorator(ILocalStorageService localStorageService, MaterialTypeService materialTypeService, ILogger<CachedMaterialTypeServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _materialTypeService = materialTypeService;
        _logger = logger;
    }

    public async Task<MaterialType> Create(CreateMaterialTypeRequest materialType)
    {
        var result = await _materialTypeService.Create(materialType);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<MaterialType> Edit(MaterialType materialType)
    {
        var result = await _materialTypeService.Edit(materialType);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _materialTypeService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<MaterialType> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<MaterialType>> ListPaged(int pageSize)
    {
        string key = "materialTypes";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<MaterialType>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading material types from local storage.");
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

        var materialTypes = await _materialTypeService.ListPaged(pageSize);
        var entry = new CacheEntry<List<MaterialType>>(materialTypes);
        await _localStorageService.SetItemAsync(key, entry);
        return materialTypes;
    }

    public async Task<List<MaterialType>> List()
    {
        string key = "materialTypes";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<MaterialType>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading material types from local storage.");
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

        var materialTypes = await _materialTypeService.List();
        var entry = new CacheEntry<List<MaterialType>>(materialTypes);
        await _localStorageService.SetItemAsync(key, entry);
        return materialTypes;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "materialTypes";

        await _localStorageService.RemoveItemAsync(key);
        var materialTypes = await _materialTypeService.List();
        var entry = new CacheEntry<List<MaterialType>>(materialTypes);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
