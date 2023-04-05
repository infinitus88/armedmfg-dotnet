using System;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class ListPagedOrderShipmentEndpoint : IEndpoint<IResult, ListPagedOrderShipmentRequest, IRepository<OrderShipment>>
{
    private readonly IMapper _mapper;

    public ListPagedOrderShipmentEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-shipments",
                async (int? pageSize, int? pageIndex, DateTime? startDate, DateTime? endDate, int? orderId, IRepository<OrderShipment> orderShipmentRepository) =>
                {
                    return await HandleAsync(new ListPagedOrderShipmentRequest(pageSize, pageIndex, startDate, endDate, orderId), orderShipmentRepository);
                })
            .Produces<ListPagedOrderShipmentResponse>()
            .WithTags("OrderShipmentEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedOrderShipmentRequest request, IRepository<OrderShipment> orderShipmentRepository)
    {
        //await Task.Delay(1000);
        var response = new ListPagedOrderShipmentResponse(request.CorrelationId());

        // var filterSpec = new OrderShipmentFilterSpecification(request.StartDate, request.EndDate, request.OrderId);
        // int totalItems = await orderShipmentRepository.CountAsync(filterSpec);
        //
        // var pagedSpec = new OrderShipmentFilterPaginatedSpecification(
        //     skip: request.PageIndex.Value * request.PageSize.Value,
        //     take: request.PageSize.Value,
        //     startDate: request.StartDate,
        //     endDate: request.EndDate,
        //     orderId: request.OrderId);
        //
        // var orders = await orderShipmentRepository.ListAsync(pagedSpec);
        //
        // response.OrderShipments.AddRange(orders.Select(((IMapperBase)_mapper).Map<OrderShipmentDto>));
        //
        // if (request.PageSize > 0)
        // {
        //     response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize.Value).ToString());
        // }
        // else
        // {
        //     response.PageCount = totalItems > 0 ? 1 : 0;
        // }

        return Results.Ok(response);
    }
}
