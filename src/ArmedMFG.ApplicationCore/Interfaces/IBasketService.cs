using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Result;
using ArmedMFG.ApplicationCore.Entities.BasketAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Interfaces;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<Basket> AddItemToBasket(string username, int catalogItemId, decimal price, int quantity = 1);
    Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
    Task DeleteBasketAsync(int basketId);
}

public interface IProductBatchService
{
    Task<ProductBatch> AddProducedProduct(int productTypeId, int quantity = 1);
    Task<ProductBatch> AddSpentMaterial(int materialTypeId, float amount = 1);
    Task DeleteProductBatchAsync(int productBatchId);
}

public interface IProductTypeService
{
    Task<ProductType> AddPriceRecord(int productTypeId, DateTime fromDate, decimal price, int quantity = 1);
    Task DeleteProductTypeAsync(int productTypeId);
}
