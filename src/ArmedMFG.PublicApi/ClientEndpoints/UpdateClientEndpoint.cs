using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ClientAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class UpdateClientEndpoint : IEndpoint<IResult, UpdateClientRequest, IRepository<Client>>
{
    private readonly IMapper _mapper;
    
    public UpdateClientEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/clients",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateClientRequest request, IRepository<Client> clientRepository) =>
                {
                    return await HandleAsync(request, clientRepository);
                })
            .Produces<UpdateClientResponse>()
            .WithTags("ClientEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateClientRequest request, IRepository<Client> clientRepository)
    {
        var response = new UpdateClientResponse(request.CorrelationId());

        var existingClient = await clientRepository.GetByIdAsync(request.Id);

        Client.ClientDetails details = new(request.FullName, request.PhoneNumber, request.Email, request.FindOutThrough);
        existingClient.UpdateDetails(details);

        await clientRepository.UpdateAsync(existingClient);

        var dto = new ClientDto
        {
            Id = existingClient.Id,
            FullName = existingClient.FullName,
            PhoneNumber = existingClient.PhoneNumber,
            Email = existingClient.Email,
            OrganizationId = existingClient.OrganizationId
        };
        response.Client = dto;
        return Results.Ok(response);
    }
}
