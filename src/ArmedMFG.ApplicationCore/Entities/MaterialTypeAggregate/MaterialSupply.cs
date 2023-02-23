using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

public class MaterialSupply : BaseEntity, IAggregateRoot
{
    public int MaterialTypeId { get; private set; }
    public MaterialType? MaterialType { get; private set; }
    public DateTime DeliveredDate { get; private set; }
    public decimal Price { get; private set; }
    public double Amount { get; private set; }

    public MaterialSupply(int materialTypeId, DateTime deliveredDate, decimal price, double amount)
    {
        MaterialTypeId = materialTypeId;
        DeliveredDate = deliveredDate;
        Price = price;
        Amount = amount;
    }

    public void UpdateDetails(MaterialSupplyDetails details)
    {
        Guard.Against.Default(details.DeliveredDate, nameof(details.DeliveredDate));
        Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));
        Guard.Against.NegativeOrZero(details.Amount, nameof(details.Amount));

        DeliveredDate = details.DeliveredDate;
        Price = details.Price;
        Amount = details.Amount;
    }
    
    public readonly record struct MaterialSupplyDetails
    {
        public DateTime DeliveredDate { get; }
        public decimal Price { get; }
        public double Amount { get; }

        public MaterialSupplyDetails(DateTime deliveredDate, decimal price, double amount)
        {
            DeliveredDate = deliveredDate;
            Price = price;
            Amount = amount;
        }
    }
}
