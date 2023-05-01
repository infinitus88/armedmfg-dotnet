using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class SearchProductBatchEndpoint : IEndpoint<IResult, SearchProductBatchRequest, IRepository<ProductBatch>>
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;

    public SearchProductBatchEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-batches/search",
                async (SearchProductBatchRequest request, IRepository<ProductBatch> productBatchRepository) =>
                {
                    return await HandleAsync(request, productBatchRepository);
                })
            .Produces<SearchProductBatchResponse>()
            .WithTags("ProductBatchEndpoints");
    }
    
    public async Task<IResult> HandleAsync(SearchProductBatchRequest request, IRepository<ProductBatch> productBatchRepository)
    {
        //await Task.Delay(1000);
        var response = new SearchProductBatchResponse(request.CorrelationId());

        var filterSpec = new ProductBatchFilterSpecification(
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));
        int totalItems = await productBatchRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductBatchFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            DateTime.ParseExact(request.Filter.StartDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact(request.Filter.EndDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture));

        var productBatches = await productBatchRepository.ListAsync(pagedSpec);

        response.ProductBatches.AddRange(productBatches.Select(((IMapperBase)_mapper).Map<ProductBatchInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
