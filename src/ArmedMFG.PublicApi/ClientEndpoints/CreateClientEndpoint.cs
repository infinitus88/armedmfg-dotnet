using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ClientAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class CreateClientEndpoint : IEndpoint<IResult, CreateClientRequest, IRepository<Client>, IRepository<Organization>>
{
    private readonly IMapper _mapper;

    public CreateClientEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/clients",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateClientRequest request, IRepository<Client> clientRepository, IRepository<Organization> organizationRepository) =>
                {
                    return await HandleAsync(request, clientRepository, organizationRepository);
                })
            .Produces<CreateClientResponse>()
            .WithTags("ClientEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateClientRequest request,
        IRepository<Client> clientRepository, IRepository<Organization> organizationRepository)
    {
        var response = new CreateClientResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        

        var newClient = new Client(request.FullName, request.PhoneNumber, request.FindOutThrough);
        newClient = await clientRepository.AddAsync(newClient);
        
        if (request.OrganizationId.HasValue)
        {
            var existingOrganization = await organizationRepository.GetByIdAsync(request.OrganizationId);
            
            if (existingOrganization == null)
            {
                throw new NotFoundException($"A organization with Id: {request.OrganizationId} not be found");
            } 
            
            newClient.SetOrganization(existingOrganization.Id);
        }

        var dto = new ClientDto
        {
            Id = newClient.Id,
            FullName = newClient.FullName,
            PhoneNumber = newClient.PhoneNumber,
            Email = newClient.Email,
            OrganizationId = newClient.OrganizationId
        };
        response.Client = dto;
        return Results.Created($"api/clients/{dto.Id}", response);
    }
}
