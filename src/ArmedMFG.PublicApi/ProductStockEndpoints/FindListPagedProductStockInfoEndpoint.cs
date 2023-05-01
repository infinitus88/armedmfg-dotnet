using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductStockEndpoints;

public class FindListPagedProductStockInfoEndpoint : IEndpoint<IResult, FindListPagedProductStockInfoRequest>
{
    private readonly IRepository<ShipmentProduct> _shipmentProductsRepository;
    private readonly IRepository<OrderProduct> _orderProductsRepository;
    private readonly IRepository<DefectiveProduct> _defectiveProductsRepository;
    private readonly IRepository<ProductCheckPoint> _checkPointsRepository;
    private readonly IRepository<ProducedProduct> _producedProductsRepository;
    private readonly IMapper _mapper;
    
    public FindListPagedProductStockInfoEndpoint(IMapper mapper, IRepository<ShipmentProduct> shipmentProductsRepository,
        IRepository<ProductCheckPoint> checkPointsRepository,
        IRepository<ProducedProduct> producedProductsRepository,
        IRepository<DefectiveProduct> defectiveRepository,
        IRepository<OrderProduct> orderProductsRepository)
    {
        _mapper = mapper;
        _shipmentProductsRepository = shipmentProductsRepository;
        _checkPointsRepository = checkPointsRepository;
        _producedProductsRepository = producedProductsRepository;
        _defectiveProductsRepository = defectiveRepository;
        _orderProductsRepository = orderProductsRepository;
    }

    public async Task<IResult> HandleAsync(FindListPagedProductStockInfoRequest request)
    {
        var response = new FindListPagedProductStockInfoResponse(request.CorrelationId());
        var producedProductsFilter = new ProducedProductFilterSpecification();
        var producedProducts = await _producedProductsRepository.ListAsync(producedProductsFilter);

        var shipmentProductsFilter = new ShipmentProductFilterSpecification();
        var shipmentProducts = await _shipmentProductsRepository.ListAsync(shipmentProductsFilter);

        var defectiveProductsFilter = new DefectiveProductFilterSpecification();
        var defectiveProducts = await _defectiveProductsRepository.ListAsync(defectiveProductsFilter);
        
        var recentCheckPointsFilter = new ProductCheckPointRecentSpecification();
        var checkPoints = await _checkPointsRepository.ListAsync(recentCheckPointsFilter);
        checkPoints = checkPoints.DistinctBy(cp => cp.ProductTypeId).ToList();

        var productStocksInfo = new List<ProductStockInfoDto>();
        
        foreach (var checkPoint in checkPoints)
        {
            productStocksInfo.Add(new ProductStockInfoDto { 
                ProductTypeId = checkPoint.ProductTypeId,
                ProductName = checkPoint.ProductType.Name,
                ProductCategoryId = checkPoint.ProductType.ProductCategoryId,
                Quantity = checkPoint.Quantity 
                           + producedProducts.Where(p => p.ProductBatch.ProducedDate.Date > checkPoint.CheckedDate.Date && p.ProductTypeId == checkPoint.ProductTypeId)
                               .Sum(p => p.Quantity)
                           - shipmentProducts.Where(sp => sp.OrderShipment.ShipmentDate > checkPoint.CheckedDate && sp.ProductTypeId == checkPoint.ProductTypeId)
                               .Sum(sp => sp.Quantity)
                           - defectiveProducts.Where(dp => dp.FoundDate > checkPoint.CheckedDate && dp.ProductTypeId == checkPoint.ProductTypeId)
                               .Sum(dp => dp.Quantity),
            });
            
        }
        
        if (request.PageSize.Value == 0)
        {
            request.PageSize = int.MaxValue;
        }
        
        productStocksInfo = productStocksInfo
            .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value).ToList();
        
        response.ProductStocks = productStocksInfo;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-stocks/find",
                async ([FromBody]FindListPagedProductStockInfoRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<FindListPagedProductStockInfoResponse>()
            .WithTags("ProductStocksEndpoint");

    }
}
