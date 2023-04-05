using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.OrderEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class FindListPagedCustomerEndpoint : IEndpoint<IResult, FindListPagedCustomerRequest, IRepository<Customer>>
{
    private readonly IMapper _mapper;

    public FindListPagedCustomerEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/customers/find",
                async (FindListPagedCustomerRequest request, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(request, customerRepository);
                })
            .Produces<FindListPagedCustomerResponse>()
            .WithTags("CustomerEndpoints");
    }
    
    public async Task<IResult> HandleAsync(FindListPagedCustomerRequest request, IRepository<Customer> customerRepository)
    {
        //await Task.Delay(1000);
        var response = new FindListPagedCustomerResponse(request.CorrelationId());

        var filterSpec = new CustomerFilterSpecification(request.Filter.FullName);
        int totalItems = await customerRepository.CountAsync(filterSpec);

        var pagedSpec = new CustomerFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.FullName);

        var customers = await customerRepository.ListAsync(pagedSpec);

        response.Customers.AddRange(customers.Select(((IMapperBase)_mapper).Map<CustomerInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
