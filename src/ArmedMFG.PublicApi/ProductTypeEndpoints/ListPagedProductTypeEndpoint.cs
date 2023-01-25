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

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class ListPagedProductTypeEndpoint : IEndpoint<IResult, ListPagedProductTypeRequest, IRepository<ProductType>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public ListPagedProductTypeEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-types",
                async (int? pageSize, int? pageIndex, int? productCategoryId, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(new ListPagedProductTypeRequest(pageSize, pageIndex, productCategoryId), productTypeRepository);
                })
            .Produces<ListPagedProductTypeResponse>()
            .WithTags("ProductTypeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedProductTypeRequest request, IRepository<ProductType> productTypeRepository)
    {
        await Task.Delay(1000);
        var response = new ListPagedProductTypeResponse(request.CorrelationId());

        var filterSpec = new ProductTypeFilterSpecification(request.ProductCategoryId);
        int totalItems = await productTypeRepository.CountAsync(filterSpec);

        var pagedSpec = new ProductTypeFilterPaginatedSpecification(
            skip: request.PageIndex.Value * request.PageSize.Value,
            take: request.PageSize.Value,
            productCategoryId: request.ProductCategoryId);

        var productTypes = await productTypeRepository.ListAsync(pagedSpec);

        response.ProductTypes.AddRange(productTypes.Select(_mapper.Map<ProductTypeDto>));
        foreach (ProductTypeDto item in response.ProductTypes)
        {
            item.PictureUri = _uriComposer.ComposePicUri(item.PictureUri);
        }

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
