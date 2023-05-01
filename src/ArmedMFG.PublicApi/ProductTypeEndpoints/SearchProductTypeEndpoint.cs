using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.ProductBatchEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class SearchProductTypeEndpoint : IEndpoint<IResult, SearchProductTypeRequest, IRepository<ProductType>>
{
    private readonly IMapper _mapper;

    public SearchProductTypeEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-types/search",
                async (SearchProductTypeRequest request, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(request, productTypeRepository);
                })
            .Produces<SearchProductTypeResponse>()
            .WithTags("ProductTypeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(SearchProductTypeRequest request, IRepository<ProductType> productTypeRepository)
    {
        //await Task.Delay(1000);
        var response = new SearchProductTypeResponse(request.CorrelationId());

        var filterSpec = new ProductTypeFilterSpecification(request.Filter.SearchText, request.Filter.ProductCategoryId);
        int totalItems = await productTypeRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductTypeFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.SearchText,
            request.Filter.ProductCategoryId);

        var productTypes = await productTypeRepository.ListAsync(pagedSpec);

        response.ProductTypes.AddRange(productTypes.Select(((IMapperBase)_mapper).Map<ProductTypeInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
