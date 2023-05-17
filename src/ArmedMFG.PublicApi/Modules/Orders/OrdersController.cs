using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using ArmedMFG.PublicApi.Configuration;
using System.Globalization;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.PublicApi.Modules.Orders.Dtos;
using ArmedMFG.ApplicationCore.Entities.CustomerAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;
using ArmedMFG.ApplicationCore.Specifications.Orders;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

namespace ArmedMFG.PublicApi.Modules.Orders;
[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<PaymentRecord> _paymentRecordRepository;

    public OrdersController(IMapper mapper, DateParsingSettings dateParsingSettings, IRepository<Order> orderRepository, IRepository<Customer> customerRepository, IRepository<PaymentRecord> paymentRecordRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _paymentRecordRepository = paymentRecordRepository;
    }

    [HttpPost]
    [Obsolete]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Create([FromBody] CreateOrderRequest request)
    {
        var response = new CreateOrderResponse(request.CorrelationId());

        var existingCustomer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (existingCustomer == null)
        {
            throw new NotFoundException($"A order's customer with Id: {request.CustomerId} is not found");
        }

        var newOrder = new Order(request.CustomerId,
            DateTime.ParseExact(request.OrderedDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.RequiredDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.TotalAmount, "");

        newOrder.SetStatus(Status.InQueue);

        foreach (var orderProduct in request.OrderProducts)
        {
            newOrder.AddOrderProduct(orderProduct.ProductId, orderProduct.Quantity, orderProduct.UnitPrice);
        }

        newOrder = await _orderRepository.AddAsync(newOrder);

        
        response.Order = _mapper.Map<OrderDto>(newOrder);
        return Results.Created($"api/orders/{newOrder.Id}", response);
    }

    //[HttpGet("{orderId}")]
    //public async Task<IResult> GetOrderDetails(int orderId)
    //{
    //    var response = new GetOrderDetailResponse();

    //    var filterSpec = new OrderDetailSpecification(orderId);

    //    var order = await _orderRepository.GetBySpecAsync(filterSpec);
    //    if (order is null)
    //    {
    //        return Results.NotFound();
    //    }

    //    var paymentsSpec = new OrderPaymentsFilterSpecification(orderId);
    //    var paymentRecords = await _paymentRecordRepository.ListAsync(paymentsSpec);

    //    response.OrderDetail = _mapper.Map<OrderDetailDto>(order);
    //    response.OrderDetail.OrderProducts.AddRange(order.OrderProducts.Select(_mapper.Map<OrderProductDto>));
    //    response.OrderDetail.OrderShipments.AddRange(order.OrderShipments.Select(((IMapperBase)_mapper).Map<OrderShipmentDto>));
    //    response.OrderDetail.OrderPaymentRecords.AddRange(paymentRecords.Select(((IMapperBase)_mapper).Map<OrderPaymentRecordDto>));

    //    response.OrderDetail.OrderProducts.ForEach(o =>
    //    {
    //        // TODO: After adding checks for user input, modify this algorithm so it will look for FirstOrDefault while calculating for product quantity
    //        // Here is prevention if user add's to some shipment duplicated products
    //        var totalQuantity = order.OrderShipments
    //            .Sum(s => s.ShipmentProducts.Where(p => p.ProductId == o.ProductId).Sum(p => p.Quantity));

    //        o.ShippedQuantity = totalQuantity;
    //    });

    //    return Results.Ok(response);
    //}

    [HttpGet("{orderId}")]
    public async Task<IResult> GetReportForEdit(int orderId)
    {
        var response = new GetOrderForEditResponse();

        var orderForEdit = await _orderRepository.GetByIdAsync(orderId);
        if (orderForEdit is null)
            return Results.NotFound();

        response.OrderForEdit = _mapper.Map<OrderForEditDto>(orderForEdit);

        return Results.Ok(response);
    }

    [HttpPost("search")]
    public async Task<IResult> Search([FromBody] SearchOrderRequest request)
    {
        var response = new SearchOrderResponse(request.CorrelationId());

        var filterSpec = new SearchOrderFilterSpecification(
            request.Filter.SearchText,
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));
        var totalItems = await _orderRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchOrderFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            request.Filter.SearchText,
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));

        var orders = await _orderRepository.ListAsync(pagedSpec);

        response.Orders.AddRange(orders.Select(_mapper.Map<OrderDto>));
        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update([FromBody] UpdateOrderRequest request)
    {
        var response = new UpdateOrderResponse(request.CorrelationId());

        var existingOrder = await _orderRepository.GetByIdAsync(request.Id);

        if (existingOrder is null)
            return Results.NotFound();

        Order.OrderDetails details = new(
            request.CustomerId,
            DateTime.ParseExact(request.OrderedDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.RequiredDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.FinishDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.Description
            );

        existingOrder.UpdateDetails(details);

        await _orderRepository.UpdateAsync(existingOrder);

        response.Order = _mapper.Map<OrderDto>(existingOrder);

        return Results.Ok(response);
    }

    [HttpDelete("{orderId}")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> SingleDelete(int orderId)
    {
        var response = new DeleteSingleOrderResponse();

        var reportToDelete = await _orderRepository.GetByIdAsync(orderId);
        if (reportToDelete is null)
            return Results.NotFound();

        await _orderRepository.DeleteAsync(reportToDelete);

        return Results.Ok(response);
    }
}
