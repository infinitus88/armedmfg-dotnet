using System;
using System.Collections.Generic;
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

namespace ArmedMFG.PublicApi.ProductBatchEndpoints.ProducedProductEndpoints;

public class ListPagedProducedProductEndpoint : IEndpoint<IResult, ListPagedProducedProductRequest, IRepository<ProducedProduct>>
{
    private readonly IMapper _mapper;

    public ListPagedProducedProductEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-batches/produced-products",
                async (DateTime? startDate, DateTime? endDate, int? productTypeId, IRepository<ProducedProduct> producedProductRepository) =>
                {
                    return await HandleAsync(new ListPagedProducedProductRequest(startDate, endDate, productTypeId), producedProductRepository);
                })
            .Produces<ListPagedProducedProductResponse>()
            .WithTags("ProducedProductEndpoints");
    }
    
    public async Task<IResult> HandleAsync(ListPagedProducedProductRequest request, IRepository<ProducedProduct> producedProductRepository)
    {
        //await Task.Delay(1000);
        var response = new ListPagedProducedProductResponse(request.CorrelationId());

        var filterSpec = new ProducedProductFilterSpecification(request.StartDate, request.EndDate, request.ProductTypeId);

        var producedProducts = await producedProductRepository.ListAsync(filterSpec);

        var producedProductsQuantity = new List<ProducedProductQuantityDto>();

        foreach (var producedProduct in producedProducts)
        {
            var producedProductQuantity =
                producedProductsQuantity.FirstOrDefault(p => p.ProductTypeId == producedProduct.ProductTypeId);
            if (producedProductQuantity is null)
            {
                producedProductsQuantity.Add(new ProducedProductQuantityDto()
                {
                    ProductTypeId = producedProduct.ProductTypeId,
                    Quantity = producedProduct.Quantity
                });
            }
            else
            {
                producedProductQuantity.Quantity += producedProduct.Quantity;
            }
        }

        response.ProducedProductsQuantity = producedProductsQuantity;

        return Results.Ok(response);
    }
}
