using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedCustomerOrganizationServiceDecorator : ICustomerOrganizationService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly CustomerOrganizationService _organizationService;
    private readonly ILogger<CachedCustomerOrganizationServiceDecorator> _logger;

    public CachedCustomerOrganizationServiceDecorator(ILocalStorageService localStorageService, CustomerOrganizationService organizationService, ILogger<CachedCustomerOrganizationServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _organizationService = organizationService;
        _logger = logger;
    }


    public async Task<CustomerOrganization> Create(CreateCustomerOrganizationRequest organization)
    {
        var result = await _organizationService.Create(organization);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<CustomerOrganization> Edit(CustomerOrganization organization)
    {
        var result = await _organizationService.Edit(organization);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _organizationService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<CustomerOrganization> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<CustomerOrganization>> ListPaged(int pageSize)
    {
        string key = "organization";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<CustomerOrganization>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading organizations from local storage.");
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

        var organizations = await _organizationService.ListPaged(pageSize);
        var entry = new CacheEntry<List<CustomerOrganization>>(organizations);
        await _localStorageService.SetItemAsync(key, entry);
        return organizations;
    }

    public async Task<List<CustomerOrganization>> List()
    {
        string key = "organizations";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<CustomerOrganization>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading organizations from local storage.");
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

        var organizations = await _organizationService.List();
        var entry = new CacheEntry<List<CustomerOrganization>>(organizations);
        await _localStorageService.SetItemAsync(key, entry);
        return organizations;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "organizations";

        await _localStorageService.RemoveItemAsync(key);
        var organizations = await _organizationService.List();
        var entry = new CacheEntry<List<CustomerOrganization>>(organizations);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
