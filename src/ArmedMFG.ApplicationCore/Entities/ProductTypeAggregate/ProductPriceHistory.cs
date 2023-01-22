using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

public class ProductPriceHistory : BaseEntity, IAggregateRoot
{
    public int ProductTypeId { get; private set; }
    public ProductType? ProductType { get; private set; }
    public DateTime FromDate { get; private set; }
    public decimal Price { get; private set; }
    
    public ProductPriceHistory(int productTypeId, DateTime fromDate, decimal price)
    {
        ProductTypeId = productTypeId;
        FromDate = fromDate;
        Price = price;
    }

    public void UpdateDetails(ProductPriceHistoryDetails details)
    {
        Guard.Against.Default(details.FromDate, nameof(details.FromDate));
        Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));

        FromDate = details.FromDate;
        Price = details.Price; 
    }
    
    public readonly record struct ProductPriceHistoryDetails
    {
        public DateTime FromDate { get; }
        public decimal Price { get; }

        public ProductPriceHistoryDetails(DateTime fromDate, decimal price)
        {
            FromDate = fromDate;
            Price = price;
        }
    }
}
