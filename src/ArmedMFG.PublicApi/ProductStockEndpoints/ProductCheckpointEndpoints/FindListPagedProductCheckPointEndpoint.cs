using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

public class FindListPagedProductCheckPointEndpoint : IEndpoint<IResult, FindListPagedProductCheckPointRequest>
{
    private readonly IRepository<ProductCheckPoint> _checkPointsRepository;
    private readonly IMapper _mapper;
    
    public FindListPagedProductCheckPointEndpoint(IMapper mapper, IRepository<ProductCheckPoint> checkPointsRepository)
    {
        _mapper = mapper;
        _checkPointsRepository = checkPointsRepository;
    }

    public async Task<IResult> HandleAsync(FindListPagedProductCheckPointRequest request)
    {
        var response = new FindListPagedProductCheckPointResponse(request.CorrelationId());
        var filterSpec =
            new ProductCheckPointFilterSpecification(request.Filter.StartDate, request.Filter.EndDate,
                request.Filter.Name);
        int totalItems = await _checkPointsRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductCheckPointFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.StartDate,
            request.Filter.EndDate,
            request.Filter.Name
        );

        var checkPoints = await _checkPointsRepository.ListAsync(pagedSpec);
        
        response.ProductCheckPoints.AddRange(checkPoints.Select(((IMapperBase)_mapper).Map<ProductCheckPointDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-stocks/check-points/find",
                async ([FromBody]FindListPagedProductCheckPointRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<FindListPagedProductCheckPointResponse>()
            .WithTags("ProductStocksEndpoint");

    }
}
