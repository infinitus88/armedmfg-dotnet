using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class UpdateOrderEndpoint : IEndpoint<IResult, UpdateOrderRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;
    
    public UpdateOrderEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/orders",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateOrderRequest request, IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(request, orderRepository);
                })
            .Produces<UpdateOrderResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderRequest request, IRepository<Order> orderRepository)
    {
        var response = new UpdateOrderResponse(request.CorrelationId());

        var existingOrder = await orderRepository.GetByIdAsync(request.Id);

        Order.OrderDetails details = new(request.CustomerId, request.OrderedDate, request.RequiredDate, request.Description);
        existingOrder.UpdateDetails(details);

        await orderRepository.UpdateAsync(existingOrder);

        var dto = new OrderDto
        {
            Id = existingOrder.Id,
            CustomerId = existingOrder.CustomerId,
            OrderedDate = existingOrder.OrderedDate,
            RequiredDate = existingOrder.RequiredDate,
            FinishedDate = existingOrder.FinishedDate,
            Status = (byte)existingOrder.Status,
            PaymentType = (byte)existingOrder.PaymentType,
            Description = existingOrder.Description,
            OrderProducts = existingOrder.OrderProducts.Select(_mapper.Map<OrderProductDto>).ToList(),
            OrderShipments = existingOrder.OrderShipments.Select(_mapper.Map<OrderShipmentDto>).ToList()
        };
        
        response.Order = dto;
        return Results.Ok(response);
    }
}
