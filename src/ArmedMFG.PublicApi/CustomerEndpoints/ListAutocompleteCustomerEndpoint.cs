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

public class ListAutocompleteCustomerEndpoint : IEndpoint<IResult, ListAutocompleteCustomerRequest, IRepository<Customer>>
{
    private readonly IMapper _mapper;

    public ListAutocompleteCustomerEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers/filter",
                async (string? fullName, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(new ListAutocompleteCustomerRequest(fullName), customerRepository);
                })
            .Produces<ListPagedCustomerResponse>()
            .WithTags("CustomerEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListAutocompleteCustomerRequest request, IRepository<Customer> customerRepository)
    {
        // await Task.Delay(1000);
        var response = new ListAutocompleteCustomerResponse(request.CorrelationId());

        var filterSpec = new CustomerAutocompleteFilterSpecification(request.FullName);

        var customers = await customerRepository.ListAsync(filterSpec);

        foreach (var customer in customers)
        {
            response.Customers.Add(new CustomerAutocompleteDto() { Id = customer.Id, FullName = customer.FullName});
        }

        return Results.Ok(response);
    } 
}
