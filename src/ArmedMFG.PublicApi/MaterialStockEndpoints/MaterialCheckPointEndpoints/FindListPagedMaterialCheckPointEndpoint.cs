using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints.MaterialCheckPointEndpoints;

public class FindListPagedMaterialCheckPointEndpoint : IEndpoint<IResult, FindListPagedMaterialCheckPointRequest>
{
    private readonly IRepository<MaterialCheckPoint> _checkPointsRepository;
    private readonly IMapper _mapper;
    
    public FindListPagedMaterialCheckPointEndpoint(IMapper mapper, IRepository<MaterialCheckPoint> checkPointsRepository)
    {
        _mapper = mapper;
        _checkPointsRepository = checkPointsRepository;
    }

    public async Task<IResult> HandleAsync(FindListPagedMaterialCheckPointRequest request)
    {
        var response = new FindListPagedMaterialCheckPointResponse(request.CorrelationId());
        var filterSpec =
            new MaterialCheckPointFilterSpecification(request.Filter.StartDate, request.Filter.EndDate,
                request.Filter.Name);
        int totalItems = await _checkPointsRepository.CountAsync(filterSpec);

        var pagedSpec = new MaterialCheckPointFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.StartDate,
            request.Filter.EndDate,
            request.Filter.Name
        );

        var checkPoints = await _checkPointsRepository.ListAsync(pagedSpec);
        
        response.MaterialCheckPoints.AddRange(checkPoints.Select(((IMapperBase)_mapper).Map<MaterialCheckPointDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/material-stocks/check-points/find",
                async ([FromBody]FindListPagedMaterialCheckPointRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<FindListPagedMaterialCheckPointResponse>()
            .WithTags("MaterialStocksEndpoint");

    }
}
