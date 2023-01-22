using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

public class MaterialSupplyHistory : BaseEntity, IAggregateRoot
{
    public int MaterialTypeId { get; private set; }
    public MaterialType? MaterialType { get; private set; }
    public DateTime DeliveredDate { get; private set; }
    public decimal UnitPrice { get; private set; }
    public float Amount { get; private set; }

    public MaterialSupplyHistory(int materialTypeId, DateTime deliveredDate, decimal unitPrice)
    {
        MaterialTypeId = materialTypeId;
        DeliveredDate = deliveredDate;
        UnitPrice = unitPrice;
    }

    public void UpdateDetails(MaterialSupplyHistoryDetails details)
    {
        Guard.Against.Default(details.DeliveredDate, nameof(details.DeliveredDate));
        Guard.Against.NegativeOrZero(details.UnitPrice, nameof(details.UnitPrice));

        DeliveredDate = details.DeliveredDate;
        UnitPrice = details.UnitPrice; 
    }
    
    public readonly record struct MaterialSupplyHistoryDetails
    {
        public DateTime DeliveredDate { get; }
        public decimal UnitPrice { get; }

        public MaterialSupplyHistoryDetails(DateTime deliveredDate, decimal unitPrice)
        {
            DeliveredDate = deliveredDate;
            UnitPrice = unitPrice;
        }
    }
}
