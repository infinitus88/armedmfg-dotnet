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

public class FindListPagedOrderShipmentEndpoint : IEndpoint<IResult, FindListPagedOrderShipmentRequest, IRepository<OrderShipment>>
{
    private readonly IMapper _mapper;

    public FindListPagedOrderShipmentEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/order-shipments/find",
                async (FindListPagedOrderShipmentRequest request, IRepository<OrderShipment> orderShipmentRepository) =>
                {
                    return await HandleAsync(request, orderShipmentRepository);
                })
            .Produces<FindListPagedOrderShipmentResponse>()
            .WithTags("OrderShipmentEndpoints");
    }
    
    public async Task<IResult> HandleAsync(FindListPagedOrderShipmentRequest request, IRepository<OrderShipment> orderShipmentRepository)
    {
        //await Task.Delay(1000);
        var response = new FindListPagedOrderShipmentResponse(request.CorrelationId());

        var filterSpec = new OrderShipmentFilterSpecification(request.Filter.StartDate, request.Filter.EndDate, request.Filter.CustomerName, request.Filter.DriverName, request.Filter.CarNumber);
        int totalItems = await orderShipmentRepository.CountAsync(filterSpec);

        var pagedSpec = new OrderShipmentFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.StartDate,
            request.Filter.EndDate,
            request.Filter.CustomerName,
            request.Filter.DriverName,
            request.Filter.CarNumber);

        var orders = await orderShipmentRepository.ListAsync(pagedSpec);

        response.OrderShipments.AddRange(orders.Select(((IMapperBase)_mapper).Map<OrderShipmentInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
