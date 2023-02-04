using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedCustomerServiceDecorator : ICustomerService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly CustomerService _customerService;
    private readonly ILogger<CachedCustomerServiceDecorator> _logger;

    public CachedCustomerServiceDecorator(ILocalStorageService localStorageService, CustomerService customerService, ILogger<CachedCustomerServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _customerService = customerService;
        _logger = logger;
    }


    public async Task<Customer> Create(CreateCustomerRequest customer)
    {
        var result = await _customerService.Create(customer);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<Customer> Edit(Customer customer)
    {
        var result = await _customerService.Edit(customer);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _customerService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<Customer> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<Customer>> ListPaged(int pageSize)
    {
        string key = "customers";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<Customer>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading customers from local storage.");
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

        var productPrices = await _customerService.ListPaged(pageSize);
        var entry = new CacheEntry<List<Customer>>(productPrices);
        await _localStorageService.SetItemAsync(key, entry);
        return productPrices;
    }

    public async Task<List<Customer>> List()
    {
        string key = "customers";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<Customer>>>(key);
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

        var customers = await _customerService.List();
        var entry = new CacheEntry<List<Customer>>(customers);
        await _localStorageService.SetItemAsync(key, entry);
        return customers;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "customers";

        await _localStorageService.RemoveItemAsync(key);
        var customers = await _customerService.List();
        var entry = new CacheEntry<List<Customer>>(customers);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
