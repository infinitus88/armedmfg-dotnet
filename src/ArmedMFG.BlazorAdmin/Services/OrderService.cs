using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class OrderService
{
    
    private readonly IProductTypeService _productTypeService;
    private readonly ICustomerService _customerService;
    
    private readonly HttpService _httpService;
    private readonly ILogger<Order> _logger;

    public OrderService(HttpService httpService, ILogger<Order> logger, IProductTypeService productTypeService, ICustomerService customerService)
    {
        _httpService = httpService;
        _logger = logger;
        _productTypeService = productTypeService;
        _customerService = customerService;
    }
    
    public async Task<Order> Create(CreateOrderRequest order)
    {
        var response = await _httpService.HttpPost<CreateOrderResponse>("orders", order);
        return response?.Order;
    }

    public async Task<Order> Edit(Order order)
    {
        return (await _httpService.HttpPut<EditOrderResult>("orders", order)).Order;
    }

    public async Task<string> Delete(int id)
    {
        return (await _httpService.HttpDelete<DeleteOrderResponse>("orders", id)).Status;
    }

    public async Task<Order> GetById(int id)
    {
        var productTypeListTask = _productTypeService.List();
        var orderGetTask = _httpService.HttpGet<EditOrderResult>($"orders/{id}");
        await Task.WhenAll(productTypeListTask, orderGetTask);
        var productTypes = productTypeListTask.Result;
        var order = orderGetTask.Result.Order;
        order.OrderProducts.ForEach(p =>
            p.ProductType = productTypes.FirstOrDefault(t => t.Id == p.ProductTypeId)?.Name);
        return order;
    }

    public async Task<List<Order>> ListPaged(int pageSize = 10)
    {
        _logger.LogInformation("Fetching orders from API.");

        var productTypeListTask = _productTypeService.List();
        var customerListTask = _customerService.List();
        var orderListTask = _httpService.HttpGet<PagedOrderResponse>($"orders?PageSize={pageSize}");
        await Task.WhenAll(orderListTask, customerListTask, orderListTask);

        var productTypes = productTypeListTask.Result;
        var customers = customerListTask.Result;
        var orders = orderListTask.Result.Orders;

        foreach (var order in orders)
        {
            order.Customer = customers.FirstOrDefault(c => c.Id == order.CustomerId)?.FullName;
            foreach (var orderProduct in order.OrderProducts)
            {
                orderProduct.ProductType =
                    productTypes.FirstOrDefault(t => t.Id == orderProduct.ProductTypeId)?.Name;
            }
        }

        return orders;
    }

    public async Task<List<Order>> List()
    {
        _logger.LogInformation("Fetching orders from API.");

        var productTypeListTask = _productTypeService.List();
        var customerListTask = _customerService.List();
        var orderListTask = _httpService.HttpGet<PagedOrderResponse>($"orders");
        await Task.WhenAll(productTypeListTask, customerListTask, orderListTask);

        var productTypes = productTypeListTask.Result;
        var customers = customerListTask.Result;
        var orders = orderListTask.Result.Orders;

        foreach (var order in orders)
        {
            order.Customer = customers.FirstOrDefault(c => c.Id == order.CustomerId)?.FullName;
            foreach (var orderProduct in order.OrderProducts)
            {
                orderProduct.ProductType =
                    productTypes.FirstOrDefault(t => t.Id == orderProduct.ProductTypeId)?.Name;
            }
        }

        return orders;
    }
}
