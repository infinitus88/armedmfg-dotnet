using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class OrderShipmentService : IOrderShipmentService
{
    private readonly IOrderService _orderService;
    private readonly IProductTypeService _productTypeService;
    
    private readonly HttpService _httpService;
    private readonly ILogger<Order> _logger;

    public OrderShipmentService(HttpService httpService, ILogger<Order> logger, IProductTypeService productTypeService, IOrderService orderService)
    {
        _httpService = httpService;
        _logger = logger;
        _productTypeService = productTypeService;
        _orderService = orderService;
    }
    
    public async Task<OrderShipment> Create(CreateOrderShipmentRequest orderShipment)
    {
        var response = await _httpService.HttpPost<CreateOrderShipmentResponse>("orders/shipments", orderShipment);
        return response?.OrderShipment;
    }

    public async Task<OrderShipment> Edit(OrderShipment orderShipment)
    {
        return (await _httpService.HttpPut<EditOrderShipmentResult>("orders/shipments", orderShipment)).OrderShipment;
    }

    public async Task<string> Delete(int id)
    {
        return (await _httpService.HttpDelete<DeleteOrderShipmentResponse>("orders/shipments", id)).Status;
    }

    public async Task<OrderShipment> GetById(int id)
    {
        var productTypeListTask = _productTypeService.List();
        var orderShipmentGetTask = _httpService.HttpGet<EditOrderShipmentResult>($"orders/shipments/{id}");
        await Task.WhenAll(productTypeListTask, orderShipmentGetTask);
        var productTypes = productTypeListTask.Result;
        var orderShipment = orderShipmentGetTask.Result.OrderShipment;
        orderShipment.ShipmentProducts.ForEach(p =>
            p.ProductType = productTypes.FirstOrDefault(t => t.Id == p.ProductTypeId)?.Name);
        return orderShipment;
    }

    public async Task<List<OrderShipment>> ListPaged(int pageSize = 10)
    {
        _logger.LogInformation("Fetching orders from API.");

        var productTypeListTask = _productTypeService.List();
        var orderShipmentListTask = _httpService.HttpGet<PagedOrderShipmentResponse>($"orders/shipments?PageSize={pageSize}");
        await Task.WhenAll(orderShipmentListTask, productTypeListTask);

        var productTypes = productTypeListTask.Result;
        var orderShipments = orderShipmentListTask.Result.OrderShipments;

        foreach (var orderShipment in orderShipments)
        {
            // orderShipment.Customer = customers.FirstOrDefault(c => c.Id == orderShipment.CustomerId)?.FullName;
            foreach (var shipmentProduct in orderShipment.ShipmentProducts)
            {
                shipmentProduct.ProductType =
                    productTypes.FirstOrDefault(t => t.Id == shipmentProduct.ProductTypeId)?.Name;
            }
        }

        return orderShipments;
    }

    public async Task<List<OrderShipment>> List()
    {
        _logger.LogInformation("Fetching order shipments from API.");

        var productTypeListTask = _productTypeService.List();
        var orderShipmentsListTask = _httpService.HttpGet<PagedOrderShipmentResponse>($"orders/shipments");
        await Task.WhenAll(productTypeListTask, orderShipmentsListTask);

        var productTypes = productTypeListTask.Result;
        var orderShipments = orderShipmentsListTask.Result.OrderShipments;

        foreach (var orderShipment in orderShipments)
        {
            // orderShipment.Customer = customers.FirstOrDefault(c => c.Id == orderShipment.CustomerId)?.FullName;
            foreach (var shipmentProduct in orderShipment.ShipmentProducts)
            {
                shipmentProduct.ProductType =
                    productTypes.FirstOrDefault(t => t.Id == shipmentProduct.ProductTypeId)?.Name;
            }
        }

        return orderShipments;
    }
}
