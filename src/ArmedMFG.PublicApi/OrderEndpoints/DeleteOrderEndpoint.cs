using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class DeleteOrderEndpoint : IEndpoint<IResult, DeleteOrderRequest, IRepository<Order>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/orders/{orderId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int orderId, IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(new DeleteOrderRequest(orderId), orderRepository);
                })
            .Produces<DeleteOrderResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteOrderRequest request, IRepository<Order> orderRepository)
    {
        var response = new DeleteOrderResponse(request.CorrelationId());

        var orderToDelete = await orderRepository.GetByIdAsync(request.OrderId);
        if (orderToDelete is null)
            return Results.NotFound();

        await orderRepository.DeleteAsync(orderToDelete);

        return Results.Ok(response);
    }
}
