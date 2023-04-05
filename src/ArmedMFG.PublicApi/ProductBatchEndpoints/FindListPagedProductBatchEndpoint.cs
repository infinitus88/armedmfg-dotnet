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

public class FindListPagedProductBatchEndpoint : IEndpoint<IResult, FindListPagedProductBatchRequest, IRepository<ProductBatch>>
{
    private readonly IMapper _mapper;

    public FindListPagedProductBatchEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-batches/find",
                async (FindListPagedProductBatchRequest request, IRepository<ProductBatch> productBatchRepository) =>
                {
                    return await HandleAsync(request, productBatchRepository);
                })
            .Produces<FindListPagedProductBatchResponse>()
            .WithTags("ProductBatchEndpoints");
    }
    
    public async Task<IResult> HandleAsync(FindListPagedProductBatchRequest request, IRepository<ProductBatch> productBatchRepository)
    {
        //await Task.Delay(1000);
        var response = new FindListPagedProductBatchResponse(request.CorrelationId());

        var filterSpec = new ProductBatchFilterSpecification(request.Filter.StartDate, request.Filter.EndDate);
        int totalItems = await productBatchRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductBatchFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.StartDate,
            request.Filter.EndDate);

        var productBatches = await productBatchRepository.ListAsync(pagedSpec);

        response.ProductBatches.AddRange(productBatches.Select(((IMapperBase)_mapper).Map<ProductBatchInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
