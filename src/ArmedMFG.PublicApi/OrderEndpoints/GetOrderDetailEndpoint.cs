using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.ProductStockEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class GetOrderDetailEndpoint : IEndpoint<IResult, GetOrderDetailRequest>
{
    private readonly IRepository<Order> _ordersRepository;
    private readonly IRepository<PaymentRecord> _paymentRecordsRepository;
    private readonly IMapper _mapper;
    
    public GetOrderDetailEndpoint(IMapper mapper, IRepository<Order> ordersRepository, IRepository<PaymentRecord> paymentRecordsRepository)
    {
        _mapper = mapper;
        _ordersRepository = ordersRepository;
        _paymentRecordsRepository = paymentRecordsRepository;
    }

    public async Task<IResult> HandleAsync(GetOrderDetailRequest request)
    {
        var response = new GetOrderDetailResponse(request.CorrelationId());

        var filterSpec = new OrderDetailSpecification(request.OrderId);

        var order = await _ordersRepository.GetBySpecAsync(filterSpec);
        if (order is null)
        {
            return Results.NotFound();
        }

        var paymentsSpec = new OrderPaymentsFilterSpecification(request.OrderId);
        var paymentRecords = await _paymentRecordsRepository.ListAsync(paymentsSpec);

        response.OrderDetail = _mapper.Map<OrderDetailDto>(order);
        response.OrderDetail.OrderProducts.AddRange(order.OrderProducts.Select(((IMapperBase)_mapper).Map<OrderProductDto>));
        response.OrderDetail.OrderShipments.AddRange(order.OrderShipments.Select(((IMapperBase)_mapper).Map<OrderShipmentDto>));
        response.OrderDetail.OrderPaymentRecords.AddRange(paymentRecords.Select(((IMapperBase)_mapper).Map<OrderPaymentRecordDto>));

        response.OrderDetail.OrderProducts.ForEach(o =>
        {
            // TODO: After adding checks for user input, modify this algorithm so it will look for FirstOrDefault while calculating for product quantity
            // Here is prevention if user add's to some shipment duplicated products
            var totalQuantity = order.OrderShipments
                .Sum(s => s.ShipmentProducts.Where(p => p.ProductTypeId == o.ProductTypeId).Sum(p => p.Quantity));

            o.ShippedQuantity = totalQuantity;
        });
            
        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/detail/{orderId}",
                async (int orderId) =>
                {
                    return await HandleAsync(new GetOrderDetailRequest(orderId));
                })
            .Produces<GetOrderDetailResponse>()
            .WithTags("OrdersEndpoint");

    }
}
