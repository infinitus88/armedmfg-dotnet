using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class DeleteCustomerEndpoint : IEndpoint<IResult, DeleteCustomerRequest, IRepository<Customer>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/customers/{customerId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int customerId, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(new DeleteCustomerRequest(customerId), customerRepository);
                })
            .Produces<DeleteCustomerResponse>()
            .WithTags("CustomerEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteCustomerRequest request, IRepository<Customer> customerRepository)
    {
        var response = new DeleteCustomerResponse(request.CorrelationId());

        var customerToDelete = await customerRepository.GetByIdAsync(request.CustomerId);
        if (customerToDelete is null)
            return Results.NotFound();

        await customerRepository.DeleteAsync(customerToDelete);

        return Results.Ok(response);
    }
}
