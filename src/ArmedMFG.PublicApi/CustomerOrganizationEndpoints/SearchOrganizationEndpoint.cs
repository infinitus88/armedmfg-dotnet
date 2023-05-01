using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.ProductBatchEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class SearchOrganizationEndpoint : IEndpoint<IResult, SearchOrganizationRequest, IRepository<CustomerOrganization>>
{
    private readonly IMapper _mapper;

    public SearchOrganizationEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/customers/organizations/search",
                async (SearchOrganizationRequest request, IRepository<CustomerOrganization> organizationRepository) =>
                {
                    return await HandleAsync(request, organizationRepository);
                })
            .Produces<SearchOrganizationResponse>()
            .WithTags("CustomerOrganizationEndpoints");
    }
    
    public async Task<IResult> HandleAsync(SearchOrganizationRequest request, IRepository<CustomerOrganization> organizationRepository)
    {
        //await Task.Delay(1000);
        var response = new SearchOrganizationResponse(request.CorrelationId());

        var filterSpec = new CustomerOrganizationFilterSpecification(request.Filter.SearchText);
        int totalItems = await organizationRepository.CountAsync(filterSpec);

        var pagedSpec = new CustomerOrganizationFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.SearchText);

        var organizations = await organizationRepository.ListAsync(pagedSpec);

        response.Organizations.AddRange(organizations.Select(((IMapperBase)_mapper).Map<OrganizationInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
