using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedMaterialSupplyServiceDecorator : IMaterialSupplyService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly MaterialSupplyService _materialSupplyService;
    private readonly ILogger<CachedMaterialSupplyServiceDecorator> _logger;

    public CachedMaterialSupplyServiceDecorator(ILocalStorageService localStorageService, MaterialSupplyService materialSupplyService, ILogger<CachedMaterialSupplyServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _materialSupplyService = materialSupplyService;
        _logger = logger;
    }


    public async Task<MaterialSupply> Create(CreateMaterialSupplyRequest materialSupply)
    {
        var result = await _materialSupplyService.Create(materialSupply);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<MaterialSupply> Edit(MaterialSupply materialSupply)
    {
        var result = await _materialSupplyService.Edit(materialSupply);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _materialSupplyService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<MaterialSupply> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<MaterialSupply>> ListPaged(int pageSize, int? materialTypeId)
    {
        string key = "materialSupplies";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<MaterialSupply>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading material supplies from local storage.");
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

        var materialSupplies = await _materialSupplyService.ListPaged(pageSize, materialTypeId);
        var entry = new CacheEntry<List<MaterialSupply>>(materialSupplies);
        await _localStorageService.SetItemAsync(key, entry);
        return materialSupplies;
    }

    public async Task<List<MaterialSupply>> List()
    {
        string key = "materialSupplies";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<MaterialSupply>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading material supplies from local storage.");
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

        var materialSupplies = await _materialSupplyService.List();
        var entry = new CacheEntry<List<MaterialSupply>>(materialSupplies);
        await _localStorageService.SetItemAsync(key, entry);
        return materialSupplies;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "materialSupplies";

        await _localStorageService.RemoveItemAsync(key);
        var materialSupplies = await _materialSupplyService.List();
        var entry = new CacheEntry<List<MaterialSupply>>(materialSupplies);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
