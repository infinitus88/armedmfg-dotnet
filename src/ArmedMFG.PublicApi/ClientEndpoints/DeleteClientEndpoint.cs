using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ClientAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class DeleteClientEndpoint : IEndpoint<IResult, DeleteClientRequest, IRepository<Client>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/clients/{clientId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int clientId, IRepository<Client> clientRepository) =>
                {
                    return await HandleAsync(new DeleteClientRequest(clientId), clientRepository);
                })
            .Produces<DeleteClientResponse>()
            .WithTags("ClientEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteClientRequest request, IRepository<Client> clientRepository)
    {
        var response = new DeleteClientResponse(request.CorrelationId());

        var clientToDelete = await clientRepository.GetByIdAsync(request.ClientId);
        if (clientToDelete is null)
            return Results.NotFound();

        await clientRepository.DeleteAsync(clientToDelete);

        return Results.Ok(response);
    }
}
