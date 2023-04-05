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

public class FindListPagedProductTypeEndpoint : IEndpoint<IResult, FindListPagedProductTypeRequest, IRepository<ProductType>>
{
    private readonly IMapper _mapper;

    public FindListPagedProductTypeEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-types/find",
                async (FindListPagedProductTypeRequest request, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(request, productTypeRepository);
                })
            .Produces<FindListPagedProductTypeResponse>()
            .WithTags("ProductTypeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(FindListPagedProductTypeRequest request, IRepository<ProductType> productTypeRepository)
    {
        //await Task.Delay(1000);
        var response = new FindListPagedProductTypeResponse(request.CorrelationId());

        var filterSpec = new ProductTypeFilterSpecification(request.Filter.Name, request.Filter.ProductCategoryId);
        int totalItems = await productTypeRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductTypeFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.Name,
            request.Filter.ProductCategoryId);

        var productTypes = await productTypeRepository.ListAsync(pagedSpec);

        response.ProductTypes.AddRange(productTypes.Select(((IMapperBase)_mapper).Map<ProductTypeInfoDto>));

        response.TotalCount = totalItems;

        return Results.Ok(response);
    }
}
