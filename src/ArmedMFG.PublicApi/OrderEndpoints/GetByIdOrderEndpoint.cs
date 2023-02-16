using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class GetByIdOrderEndpoint : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public GetByIdOrderEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/{orderId}",
                async (int orderId, IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(new GetByIdOrderRequest(orderId), orderRepository);
                })
            .Produces<GetByIdOrderResponse>()
            .WithTags("OrderEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdOrderRequest request, IRepository<Order> orderRepository)
    {
        var response = new GetByIdOrderResponse(request.CorrelationId());

        var order = await orderRepository.GetByIdAsync(request.OrderId);
        if (order is null)
            return Results.NotFound();

        response.Order = new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderedDate = order.OrderedDate,
            RequiredDate = order.RequiredDate,
            FinishedDate = order.FinishedDate,
            Status = (byte)order.Status,
            Description = order.Description,
            OrderProducts = order.OrderProducts.Select(_mapper.Map<OrderProductDto>).ToList(),
            OrderShipments = order.OrderShipments.Select(_mapper.Map<OrderShipmentDto>).ToList()
        };

        return Results.Ok(response);
    }
}
