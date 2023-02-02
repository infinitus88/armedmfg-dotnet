using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.CustomerEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class DeleteCustomerOrganizationEndpoint : IEndpoint<IResult, DeleteCustomerOrganizationRequest, IRepository<CustomerOrganization>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/customers/organizations/{organizationId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int organizationId, IRepository<CustomerOrganization> organizationRepository) =>
                {
                    return await HandleAsync(new DeleteCustomerOrganizationRequest(organizationId), organizationRepository);
                })
            .Produces<DeleteCustomerOrganizationResponse>()
            .WithTags("CustomerOrganizationEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteCustomerOrganizationRequest request, IRepository<CustomerOrganization> organizationRepository)
    {
        var response = new DeleteCustomerOrganizationResponse(request.CorrelationId());

        var organizationToDelete = await organizationRepository.GetByIdAsync(request.OrganizationId);
        if (organizationToDelete is null)
            return Results.NotFound();

        await organizationRepository.DeleteAsync(organizationToDelete);

        return Results.Ok(response);
    }
}
