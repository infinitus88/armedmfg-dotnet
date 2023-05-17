using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications.MaterialSupplies;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Endpoints;

public class SearchMaterialSupplyEndpoint : IEndpoint<IResult, SearchMaterialSupplyRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<MaterialSupply> _materialSupplyRepository;

    public SearchMaterialSupplyEndpoint(IMapper mapper, IRepository<MaterialSupply> materialSupplyRepository)
    {
        _mapper = mapper;
        _materialSupplyRepository = materialSupplyRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/materials/supplies/search",
                async (SearchMaterialSupplyRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<SearchMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }

    public async Task<IResult> HandleAsync(SearchMaterialSupplyRequest request)
    {
        var response = new SearchMaterialSupplyResponse(request.CorrelationId());

        var filterSpec = new SearchMaterialSupplyFilterSpecification(request.Filter.MaterialId);
        var totalItems = await _materialSupplyRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchMaterialSupplyFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            request.Filter.MaterialId);

        var materialSupplys = await _materialSupplyRepository.ListAsync(pagedSpec);

        response.MaterialSupplies.AddRange(materialSupplys.Select(_mapper.Map<MaterialSupplyDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
