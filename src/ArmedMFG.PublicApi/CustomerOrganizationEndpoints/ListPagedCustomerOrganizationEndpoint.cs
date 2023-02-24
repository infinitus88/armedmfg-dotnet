using System;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.CustomerEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class ListPagedCustomerOrganizationEndpoint : IEndpoint<IResult, ListPagedCustomerOrganizationRequest, IRepository<CustomerOrganization>>
{
    private readonly IMapper _mapper;

    public ListPagedCustomerOrganizationEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers/organizations",
                async (int? pageSize, int? pageIndex, IRepository<CustomerOrganization> organizationRepository) =>
                {
                    return await HandleAsync(new ListPagedCustomerOrganizationRequest(pageSize, pageIndex), organizationRepository);
                })
            .Produces<ListPagedCustomerOrganizationResponse>()
            .WithTags("CustomerOrganizationEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedCustomerOrganizationRequest request, IRepository<CustomerOrganization> organizationRepository)
    {
        //await Task.Delay(1000);
        var response = new ListPagedCustomerOrganizationResponse(request.CorrelationId());

        int totalItems = await organizationRepository.CountAsync();

        var pagedSpec = new CustomerOrganizationFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value
            );

        var organizations = await organizationRepository.ListAsync(pagedSpec);

        response.Organizations.AddRange(organizations.Select(((IMapperBase)_mapper).Map<CustomerOrganizationDto>));

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
