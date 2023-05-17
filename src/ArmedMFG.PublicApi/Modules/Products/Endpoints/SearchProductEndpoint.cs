using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications.Products;
using ArmedMFG.PublicApi.Modules.Products.Dtos;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Products.Endpoints;

public class SearchProductEndpoint : IEndpoint<IResult, SearchProductRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Product> _productRepository;

    public SearchProductEndpoint(IMapper mapper, IRepository<Product> productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products/search",
                async (SearchProductRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<SearchProductResponse>()
            .WithTags("ProductEndpoints");
    }

    public async Task<IResult> HandleAsync(SearchProductRequest request)
    {
        var response = new SearchProductResponse(request.CorrelationId());

        var filterSpec = new SearchProductFilterSpecification(request.Filter.SearchText, request.Filter.ProductCategoryId);
        var totalItems = await _productRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchProductFilterPaginatedSpecification(
            skip: (request.PageNumber - 1) * request.PageSize,
            take: request.PageSize,
            request.Filter.SearchText,
            request.Filter.ProductCategoryId);

        var products = await _productRepository.ListAsync(pagedSpec);

        response.Products.AddRange(products.Select(_mapper.Map<ProductDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
