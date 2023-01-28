using System;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class ListPagedProductPriceEndpoint : IEndpoint<IResult, ListPagedProductPriceRequest, IRepository<ProductPrice>>
{
    private readonly IMapper _mapper;

    public ListPagedProductPriceEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-types/prices",
                async (int? pageSize, int? pageIndex, int? productTypeId, IRepository<ProductPrice> productPriceRepository) =>
                {
                    return await HandleAsync(new ListPagedProductPriceRequest(pageSize, pageIndex, productTypeId), productPriceRepository);
                })
            .Produces<ListPagedProductPriceResponse>()
            .WithTags("ProductPriceEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedProductPriceRequest request, IRepository<ProductPrice> productPriceRepository)
    {
        await Task.Delay(1000);
        var response = new ListPagedProductPriceResponse(request.CorrelationId());

        var filterSpec = new ProductPriceFilterSpecification(request.ProductTypeId);
        int totalItems = await productPriceRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductPriceFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            productTypeId: request.ProductTypeId);

        var productPrices = await productPriceRepository.ListAsync(pagedSpec);

        response.ProductPrices.AddRange(productPrices.Select(((IMapperBase)_mapper).Map<ProductPriceDto>));

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
