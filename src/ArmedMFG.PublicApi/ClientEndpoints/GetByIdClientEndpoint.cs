using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ClientAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class GetByIdClientEndpoint : IEndpoint<IResult, GetByIdClientRequest, IRepository<Client>>
{
    private readonly IUriComposer _uriComposer;

    public GetByIdClientEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/clients/{clientId}",
                async (int clientId, IRepository<Client> clientRepository) =>
                {
                    return await HandleAsync(new GetByIdClientRequest(clientId), clientRepository);
                })
            .Produces<GetByIdClientResponse>()
            .WithTags("ClientEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdClientRequest request, IRepository<Client> clientRepository)
    {
        var response = new GetByIdClientResponse(request.CorrelationId());

        var client = await clientRepository.GetByIdAsync(request.ClientId);
        if (client is null)
            return Results.NotFound();

        response.Client = new ClientDto
        {
            Id = client.Id,
            FullName = client.FullName,
            PhoneNumber = client.PhoneNumber,
            Email = client.Email,
            OrganizationId = client.OrganizationId
        };

        return Results.Ok(response);
    }
}
