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

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class ListPagedMaterialSupplyEndpoint : IEndpoint<IResult, ListPagedMaterialSupplyRequest, IRepository<MaterialSupply>>
{
    private readonly IMapper _mapper;

    public ListPagedMaterialSupplyEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/material-types/supplies",
                async (int? pageSize, int? pageIndex, int? materialTypeId, IRepository<MaterialSupply> materialSupplyRepository) =>
                {
                    return await HandleAsync(new ListPagedMaterialSupplyRequest(pageSize, pageIndex, materialTypeId), materialSupplyRepository);
                })
            .Produces<ListPagedMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedMaterialSupplyRequest request, IRepository<MaterialSupply> materialSupplyRepository)
    {
        //await Task.Delay(1000);
        var response = new ListPagedMaterialSupplyResponse(request.CorrelationId());

        var filterSpec = new MaterialSupplyFilterSpecification(request.MaterialTypeId);
        int totalItems = await materialSupplyRepository.CountAsync(filterSpec);

        var pagedSpec = new MaterialSupplyFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            materialTypeId: request.MaterialTypeId);

        var materialSupplys = await materialSupplyRepository.ListAsync(pagedSpec);

        response.MaterialSupplies.AddRange(materialSupplys.Select(((IMapperBase)_mapper).Map<MaterialSupplyDto>));

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
