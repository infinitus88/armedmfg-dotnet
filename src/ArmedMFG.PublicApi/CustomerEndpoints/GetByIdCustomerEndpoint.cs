using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class GetByIdCustomerEndpoint : IEndpoint<IResult, GetByIdCustomerRequest, IRepository<Customer>>
{
    private readonly IUriComposer _uriComposer;

    public GetByIdCustomerEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers/{customerId}",
                async (int customerId, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(new GetByIdCustomerRequest(customerId), customerRepository);
                })
            .Produces<GetByIdCustomerResponse>()
            .WithTags("CustomerEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdCustomerRequest request, IRepository<Customer> customerRepository)
    {
        var response = new GetByIdCustomerResponse(request.CorrelationId());

        var customer = await customerRepository.GetByIdAsync(request.CustomerId);
        if (customer is null)
            return Results.NotFound();

        response.Customer = new CustomerDto
        {
            Id = customer.Id,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            FindOutThrough = customer.FindOutThrough,
            OrganizationId = customer.OrganizationId
        };

        return Results.Ok(response);
    }
}
