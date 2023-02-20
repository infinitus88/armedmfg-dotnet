using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.WarehouseAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.WarehouseEndpoints;

public class ListWarehouseProductItemInfoEndpoint : IEndpoint<IResult, ListWarehouseProductItemInfoRequest, IRepository<WarehouseProductCheckPoint>, IRepository<ProducedProduct>>
{
    public async Task<IResult> HandleAsync(ListWarehouseProductItemInfoRequest request, IRepository<WarehouseProductCheckPoint> warehouseProductCpRepository, IRepository<ProducedProduct> producedProductRepository)
    {
        var response = new ListWarehouseProductItemInfoResponse(request.CorrelationId());
        var producedProductsFilter = new ProducedProductFilterSpecification();
        var producedProducts = await producedProductRepository.ListAsync(producedProductsFilter);
        
        var recentCheckPointsFilter = new WarehouseProductCheckPointRecentSpecification();
        var checkPoints = await warehouseProductCpRepository.ListAsync(recentCheckPointsFilter);
        checkPoints = checkPoints.DistinctBy(cp => cp.ProductTypeId).ToList();

        var warehouseProductItems = new List<WarehouseItemInfoDto>();
        foreach (var checkPoint in checkPoints)
        {
            warehouseProductItems.Add(new WarehouseItemInfoDto { 
                ProductTypeId = checkPoint.ProductTypeId, 
                Quantity = checkPoint.Quantity 
                           + producedProducts.Where(p => p.ProductBatch.ProducedDate.Date > checkPoint.CheckedDate.Date && p.ProductTypeId == checkPoint.ProductTypeId)
                               .Sum(p => p.Quantity) 
            });
        }
        
        response.WarehouseItemInfo = warehouseProductItems;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/warehouse-products/quantity",
                async (int? productCategoryId, IRepository<WarehouseProductCheckPoint> warehouseProductCpRepository,
                    IRepository<ProducedProduct> producedProductRepository) =>
                {
                    return await HandleAsync(new ListWarehouseProductItemInfoRequest(productCategoryId),
                        warehouseProductCpRepository, producedProductRepository);
                })
            .Produces<ListWarehouseProductItemInfoResponse>()
            .WithTags("WarehouseProductItemEndpoint");

    }
}
