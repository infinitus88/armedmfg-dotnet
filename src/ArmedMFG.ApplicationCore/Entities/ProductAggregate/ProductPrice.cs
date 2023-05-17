using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;
using JetBrains.Annotations;

namespace ArmedMFG.ApplicationCore.Entities.ProductAggregate;

public class ProductPrice : BaseEntity, IAggregateRoot
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }
    public DateTime FromDate { get; private set; }
    public decimal Price { get; private set; }
    
    public ProductPrice(int productTypeId, DateTime fromDate, decimal price)
    {
        ProductId = productTypeId;
        FromDate = fromDate;
        Price = price;
    }

    public void UpdateDetails(ProductPriceDetails details)
    {
        Guard.Against.Default(details.FromDate, nameof(details.FromDate));
        Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));

        FromDate = details.FromDate;
        Price = details.Price; 
    }
    
    public readonly record struct ProductPriceDetails
    {
        public DateTime FromDate { get; }
        public decimal Price { get; }

        public ProductPriceDetails(DateTime fromDate, decimal price)
        {
            FromDate = fromDate;
            Price = price;
        }
    }
}
