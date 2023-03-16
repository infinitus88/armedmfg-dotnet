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

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class FindListPagedOrderEndpoint : IEndpoint<IResult, FindListPagedOrderRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public FindListPagedOrderEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/orders/find",
                async (FindListPagedOrderRequest request, IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(request, orderRepository);
                })
            .Produces<FindListPagedOrderResponse>()
            .WithTags("OrderEndpoints");
    }
    
    public async Task<IResult> HandleAsync(FindListPagedOrderRequest request, IRepository<Order> orderRepository)
    {
        //await Task.Delay(1000);
        var response = new FindListPagedOrderResponse(request.CorrelationId());

        var filterSpec = new OrderFilterSpecification(request.Filter.StartDate, request.Filter.EndDate, request.Filter.CustomerName);
        int totalItems = await orderRepository.CountAsync(filterSpec);

        var pagedSpec = new OrderFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.StartDate,
            request.Filter.EndDate,
            request.Filter.CustomerName);

        var orders = await orderRepository.ListAsync(pagedSpec);

        response.Orders.AddRange(orders.Select(((IMapperBase)_mapper).Map<OrderInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
