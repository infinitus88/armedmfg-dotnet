using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class GetByIdCustomerOrganizationEndpoint : IEndpoint<IResult, GetByIdCustomerOrganizationRequest, IRepository<CustomerOrganization>>
{
    private readonly IUriComposer _uriComposer;

    public GetByIdCustomerOrganizationEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers/organizations/{organizationId}",
                async (int organizationId, IRepository<CustomerOrganization> organizationRepository) =>
                {
                    return await HandleAsync(new GetByIdCustomerOrganizationRequest(organizationId), organizationRepository);
                })
            .Produces<GetByIdCustomerOrganizationResponse>()
            .WithTags("CustomerOrganizationEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdCustomerOrganizationRequest request, IRepository<CustomerOrganization> organizationRepository)
    {
        var response = new GetByIdCustomerOrganizationResponse(request.CorrelationId());

        var organization = await organizationRepository.GetByIdAsync(request.OrganizationId);
        if (organization is null)
            return Results.NotFound();

        response.Organization = new CustomerOrganizationDto()
        {
            Id = organization.Id,
            Name = organization.Name,
            PhoneNumber = organization.PhoneNumber,
            Email = organization.Email,
            Description = organization.Description
        };

        response.Organization.Description = organization.MainBranchAddress.ToString();

        return Results.Ok(response);
    }
}
