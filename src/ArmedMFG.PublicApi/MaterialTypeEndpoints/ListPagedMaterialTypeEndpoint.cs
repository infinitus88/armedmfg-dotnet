using System;
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

public class ListPagedMaterialTypeEndpoint : IEndpoint<IResult, ListPagedMaterialTypeRequest, IRepository<MaterialType>>
{
    private readonly IMapper _mapper;

    public ListPagedMaterialTypeEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/material-types",
                async (int? pageSize, int? pageIndex, int? materialCategoryId, IRepository<MaterialType> materialTypeRepository) =>
                {
                    return await HandleAsync(new ListPagedMaterialTypeRequest(pageSize, pageIndex, materialCategoryId), materialTypeRepository);
                })
            .Produces<ListPagedMaterialTypeResponse>()
            .WithTags("MaterialTypeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository)
    {
        //await Task.Delay(1000);
        var response = new ListPagedMaterialTypeResponse(request.CorrelationId());

        var filterSpec = new MaterialTypeFilterSpecification(request.MaterialCategoryId);
        int totalItems = await materialTypeRepository.CountAsync(filterSpec);

        var pagedSpec = new MaterialTypeFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            materialCategoryId: request.MaterialCategoryId);

        var materialTypes = await materialTypeRepository.ListAsync(pagedSpec);

        response.MaterialTypes.AddRange(materialTypes.Select(_mapper.Map<MaterialTypeDto>));

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
