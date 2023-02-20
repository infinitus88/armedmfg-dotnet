using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CachedOrderShipmentServiceDecorator : IOrderShipmentService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly OrderShipmentService _orderShipmentService;
    private ILogger<CachedOrderServiceDecorator> _logger;

    public CachedOrderShipmentServiceDecorator(ILocalStorageService localStorageService, OrderShipmentService orderShipmentService, ILogger<CachedOrderServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _orderShipmentService = orderShipmentService;
        _logger = logger;
    }

    public async Task<OrderShipment> Create(CreateOrderShipmentRequest orderShipment)
    {
        var result = await _orderShipmentService.Create(orderShipment);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<OrderShipment> Edit(OrderShipment orderShipment)
    {
        var result = await _orderShipmentService.Edit(orderShipment);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var result = await _orderShipmentService.Delete(id);
        await RefreshLocalStorageList();

        return result;
    }

    public async Task<OrderShipment> GetById(int id)
    {
        return (await List()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<OrderShipment>> ListPaged(int pageSize)
    {
        string key = "orderShipments";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<OrderShipment>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading order shipments from local storage.");
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

        var orderShipments = await _orderShipmentService.ListPaged(pageSize);
        var entry = new CacheEntry<List<OrderShipment>>(orderShipments);
        await _localStorageService.SetItemAsync(key, entry);
        return orderShipments;
    }

    public async Task<List<OrderShipment>> List()
    {
        string key = "orderShipments";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<OrderShipment>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading order shipments from local storage.");
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

        var orderShipments = await _orderShipmentService.List();
        var entry = new CacheEntry<List<OrderShipment>>(orderShipments);
        await _localStorageService.SetItemAsync(key, entry);
        return orderShipments;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "orderShipments";

        await _localStorageService.RemoveItemAsync(key);
        var orderShipments = await _orderShipmentService.List();
        var entry = new CacheEntry<List<OrderShipment>>(orderShipments);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
