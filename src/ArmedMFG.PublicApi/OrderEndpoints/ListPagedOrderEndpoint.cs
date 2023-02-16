using System;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.OrderEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class ListPagedOrderEndpoint : IEndpoint<IResult, ListPagedOrderRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public ListPagedOrderEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders",
                async (int? pageSize, int? pageIndex, int? customerId, IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(new ListPagedOrderRequest(pageSize, pageIndex, customerId), orderRepository);
                })
            .Produces<ListPagedOrderResponse>()
            .WithTags("OrderEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedOrderRequest request, IRepository<Order> orderRepository)
    {
        await Task.Delay(1000);
        var response = new ListPagedOrderResponse(request.CorrelationId());

        var filterSpec = new OrderFilterSpecification(request.CustomerId);
        int totalItems = await orderRepository.CountAsync(filterSpec);

        var pagedSpec = new OrderFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            customerId: request.CustomerId);

        var orders = await orderRepository.ListAsync(pagedSpec);

        response.Orders.AddRange(orders.Select(((IMapperBase)_mapper).Map<OrderDto>));

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize.Value).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }
}
