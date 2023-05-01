using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class GetByIdOrderEndpoint : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;

    public GetByIdOrderEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
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

        var filterSpec = new OrderWithProductsSpecification(request.OrderId);

        var order = await orderRepository.GetByIdAsync(filterSpec);
        
        if (order is null)
            return Results.NotFound();

        response.Order = new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderedDate = order.OrderedDate.ToString(_dateParsingSettings.DefaultInputDateFormat),
            RequiredDate = order.RequiredDate.ToString(_dateParsingSettings.DefaultInputDateFormat),
            FinishedDate = order.FinishedDate?.ToString(_dateParsingSettings.DefaultInputDateFormat),
            Status = (byte)order.Status,
            Description = order.Description,
            OrderProducts = order.OrderProducts.Select(_mapper.Map<OrderProductDto>).ToList(),
            OrderShipments = order.OrderShipments.Select(_mapper.Map<OrderShipmentDto>).ToList()
        };

        return Results.Ok(response);
    }
}
