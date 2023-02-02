using System;
using System.Linq;
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

public class ListPagedCustomerEndpoint : IEndpoint<IResult, ListPagedCustomerRequest, IRepository<Customer>>
{
    private readonly IMapper _mapper;

    public ListPagedCustomerEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers",
                async (int? pageSize, int? pageIndex, int? organizationId, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(new ListPagedCustomerRequest(pageSize, pageIndex, organizationId), customerRepository);
                })
            .Produces<ListPagedCustomerResponse>()
            .WithTags("CustomerEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedCustomerRequest request, IRepository<Customer> customerRepository)
    {
        await Task.Delay(1000);
        var response = new ListPagedCustomerResponse(request.CorrelationId());

        var filterSpec = new CustomerFilterSpecification(request.OrganizationId);
        int totalItems = await customerRepository.CountAsync(filterSpec);

        var pagedSpec = new CustomerFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            organizationId: request.OrganizationId);

        var customers = await customerRepository.ListAsync(pagedSpec);

        response.Customers.AddRange(customers.Select(((IMapperBase)_mapper).Map<CustomerDto>));

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize.Value).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }
}
