using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

public class MaterialSupply : BaseEntity, IAggregateRoot
{
    public int MaterialTypeId { get; private set; }
    public MaterialType? MaterialType { get; private set; }
    public DateTime DeliveredDate { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Amount { get; private set; }

    public MaterialSupply(int materialTypeId, DateTime deliveredDate, decimal unitPrice, decimal amount)
    {
        MaterialTypeId = materialTypeId;
        DeliveredDate = deliveredDate;
        UnitPrice = unitPrice;
        Amount = amount;
    }

    public void UpdateDetails(MaterialSupplyDetails details)
    {
        Guard.Against.Default(details.DeliveredDate, nameof(details.DeliveredDate));
        Guard.Against.NegativeOrZero(details.UnitPrice, nameof(details.UnitPrice));

        DeliveredDate = details.DeliveredDate;
        UnitPrice = details.UnitPrice; 
    }
    
    public readonly record struct MaterialSupplyDetails
    {
        public DateTime DeliveredDate { get; }
        public decimal UnitPrice { get; }
        public decimal Amount { get; }

        public MaterialSupplyDetails(DateTime deliveredDate, decimal unitPrice, decimal amount)
        {
            DeliveredDate = deliveredDate;
            UnitPrice = unitPrice;
            Amount = amount;
        }
    }
}
