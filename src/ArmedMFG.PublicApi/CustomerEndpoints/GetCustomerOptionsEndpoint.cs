using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class GetCustomerOptionsEndpoint : IEndpoint<IResult, GetCustomerOptionsRequest, IRepository<Customer>>
{
    private readonly IMapper _mapper;

    public GetCustomerOptionsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers/input-options",
                async (string? fullName, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(new GetCustomerOptionsRequest(fullName), customerRepository);
                })
            .Produces<GetCustomerOptionsResponse>()
            .WithTags("CustomerEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetCustomerOptionsRequest request, IRepository<Customer> customerRepository)
    {
        // await Task.Delay(1000);
        var response = new GetCustomerOptionsResponse(request.CorrelationId());

        var filterSpec = new CustomerAutocompleteFilterSpecification(request.FullName);

        var customers = await customerRepository.ListAsync(filterSpec);

        foreach (var customer in customers)
        {
            response.Customers.Add(new CustomerOptionDto() { Id = customer.Id, FullName = customer.FullName});
        }

        return Results.Ok(response);
    } 
}
