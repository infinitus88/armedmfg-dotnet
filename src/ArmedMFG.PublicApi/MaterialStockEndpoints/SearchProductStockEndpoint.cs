using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.PublicApi.ProductStockEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints;

public class FindListPagedMaterialStockInfoEndpoint : IEndpoint<IResult, SearchMaterialStockRequest>
{
    private readonly IRepository<MaterialCheckPoint> _checkPointsRepository;
    private readonly IRepository<MaterialSupply> _materialSupplyRepository;
    private readonly IRepository<SpentMaterial> _spentMaterialsRepository;
    private readonly IMapper _mapper;
    
    public FindListPagedMaterialStockInfoEndpoint(IMapper mapper, IRepository<MaterialCheckPoint> checkPointsRepository,
        IRepository<SpentMaterial> spentMaterialsRepository, IRepository<MaterialSupply> materialSupplyRepository)
    {
        _mapper = mapper;
        _checkPointsRepository = checkPointsRepository;
        _spentMaterialsRepository = spentMaterialsRepository;
        _materialSupplyRepository = materialSupplyRepository;
    }

    public async Task<IResult> HandleAsync(SearchMaterialStockRequest request)
    {
        var response = new SearchMaterialStockResponse(request.CorrelationId());
        var spentMaterialsFilter = new SpentMaterialFilterSpecification();
        var spentMaterials = await _spentMaterialsRepository.ListAsync(spentMaterialsFilter);

        var materialSuppliesFilter = new MaterialSupplyFilterSpecification();
        var materialSupplies = await _materialSupplyRepository.ListAsync(materialSuppliesFilter);
        
        var recentCheckPointsFilter = new MaterialCheckPointRecentSpecification();
        var checkPoints = await _checkPointsRepository.ListAsync(recentCheckPointsFilter);
        checkPoints = checkPoints.DistinctBy(cp => cp.MaterialTypeId).ToList();

        var materialStocksInfo = new List<MaterialStockInfoDto>();
        
        foreach (var checkPoint in checkPoints)
        {
            materialStocksInfo.Add(new MaterialStockInfoDto { 
                MaterialTypeId = checkPoint.MaterialTypeId,
                MaterialName = checkPoint.MaterialType.Name,
                MaterialCategoryId = checkPoint.MaterialType.MaterialCategoryId,
                Amount = checkPoint.Amount 
                           + materialSupplies.Where(ms => ms.DeliveredDate > checkPoint.CheckedDate && ms.MaterialTypeId == checkPoint.MaterialTypeId)
                               .Sum(sp => sp.Amount)
                           - spentMaterials.Where(p => p.ProductBatch.ProducedDate.Date > checkPoint.CheckedDate.Date && p.MaterialTypeId == checkPoint.MaterialTypeId)
                               .Sum(p => p.Amount)
            });
            
        }
        
        if (request.PageSize.Value == 0)
        {
            request.PageSize = int.MaxValue;
        }
        
        materialStocksInfo = materialStocksInfo
            .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value).ToList();
        
        response.MaterialStocks = materialStocksInfo;

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/material-stocks/search",
                async ([FromBody]SearchMaterialStockRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<SearchMaterialStockResponse>()
            .WithTags("MaterialStocksEndpoint");

    }
}
