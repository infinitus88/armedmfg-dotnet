using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class FindListPagedMaterialTypeEndpoint : IEndpoint<IResult, FindListPagedMaterialTypeRequest, IRepository<MaterialType>>
{
    private readonly IMapper _mapper;

    public FindListPagedMaterialTypeEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/material-types/find",
                async (FindListPagedMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository) =>
                {
                    return await HandleAsync(request, materialTypeRepository);
                })
            .Produces<FindListPagedMaterialTypeResponse>()
            .WithTags("MaterialTypeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(FindListPagedMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository)
    {
        //await Task.Delay(1000);
        var response = new FindListPagedMaterialTypeResponse(request.CorrelationId());

        var filterSpec = new MaterialTypeFilterSpecification(request.Filter.Name);
        int totalItems = await materialTypeRepository.CountAsync(filterSpec);

        var pagedSpec = new MaterialTypeFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.Name);

        var materialTypes = await materialTypeRepository.ListAsync(pagedSpec);

        response.MaterialTypes.AddRange(materialTypes.Select(((IMapperBase)_mapper).Map<MaterialTypeDto>));

        response.TotalCount = totalItems;

        // if (request.PageSize > 0)
        // {
        //     response.TotalCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize.Value).ToString());
        // }
        // else
        // {
        //     response.PageCount = totalItems > 0 ? 1 : 0;
        // }

        return Results.Ok(response);
    }
}
