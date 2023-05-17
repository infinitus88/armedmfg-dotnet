using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications.Materials;
using ArmedMFG.PublicApi.Modules.Materials.Dtos;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Materials.Endpoints;

public class SearchMaterialEndpoint : IEndpoint<IResult, SearchMaterialRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Material> _materialRepository;

    public SearchMaterialEndpoint(IMapper mapper, IRepository<Material> materialRepository)
    {
        _mapper = mapper;
        _materialRepository = materialRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/materials/search",
                async (SearchMaterialRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<SearchMaterialResponse>()
            .WithTags("MaterialEndpoints");
    }

    public async Task<IResult> HandleAsync(SearchMaterialRequest request)
    {
        var response = new SearchMaterialResponse(request.CorrelationId());

        var filterSpec = new SearchMaterialFilterSpecification(request.Filter.SearchText, request.Filter.MaterialCategoryId);
        int totalItems = await _materialRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchMaterialFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            request.Filter.SearchText,
            request.Filter.MaterialCategoryId);

        var materials = await _materialRepository.ListAsync(pagedSpec);

        response.Materials.AddRange(materials.Select(_mapper.Map<MaterialDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
