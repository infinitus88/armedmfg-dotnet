using System;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class ListPagedProductBatchEndpoint : IEndpoint<IResult, ListPagedProductBatchRequest, IRepository<ProductBatch>>
{
    private readonly IMapper _mapper;

    public ListPagedProductBatchEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-batches",
                async (int? pageSize, int? pageIndex, DateTime? startDate, DateTime? endDate, IRepository<ProductBatch> productBatchRepository) =>
                {
                    return await HandleAsync(new ListPagedProductBatchRequest(pageSize, pageIndex, startDate, endDate), productBatchRepository);
                })
            .Produces<ListPagedProductBatchResponse>()
            .WithTags("ProductBatchEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedProductBatchRequest request, IRepository<ProductBatch> productBatchRepository)
    {
        await Task.Delay(1000);
        var response = new ListPagedProductBatchResponse(request.CorrelationId());

        var filterSpec = new ProductBatchFilterSpecification(request.StartDate, request.EndDate);
        int totalItems = await productBatchRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductBatchFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            startDate: request.StartDate,
            endDate: request.EndDate
        );

        var productBatches = await productBatchRepository.ListAsync(pagedSpec);

        response.ProductBatches.AddRange(productBatches.Select(((IMapperBase)_mapper).Map<ProductBatchDto>));

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
