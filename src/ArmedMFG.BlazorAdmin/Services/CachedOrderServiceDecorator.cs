using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedOrderServiceDecorator : IOrderService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly OrderService _orderService;
    private ILogger<CachedOrderServiceDecorator> _logger;

    public CachedOrderServiceDecorator(ILocalStorageService localStorageService, OrderService orderService, ILogger<CachedOrderServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<Order> Create(CreateOrderRequest order)
    {
        var result = await _orderService.Create(order);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<Order> Edit(Order order)
    {
        var result = await _orderService.Edit(order);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _orderService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<Order> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<Order>> ListPaged(int pageSize)
    {
        string key = "orders";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<Order>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading orders from local storage.");
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

        var orders = await _orderService.ListPaged(pageSize);
        var entry = new CacheEntry<List<Order>>(orders);
        await _localStorageService.SetItemAsync(key, entry);
        return orders;
    }

    public async Task<List<Order>> List()
    {
        string key = "orders";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<Order>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading orders from local storage.");
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

        var orders = await _orderService.List();
        var entry = new CacheEntry<List<Order>>(orders);
        await _localStorageService.SetItemAsync(key, entry);
        return orders;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "orders";

        await _localStorageService.RemoveItemAsync(key);
        var orders = await _orderService.List();
        var entry = new CacheEntry<List<Order>>(orders);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
