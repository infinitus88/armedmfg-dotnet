using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;
using JetBrains.Annotations;

namespace ArmedMFG.ApplicationCore.Entities.MaterialAggregate;



public class MaterialSupply : BaseEntity, IAggregateRoot
{
    public int MaterialId { get; private set; }
    public Material? Material { get; private set; }
    public DateTime DeliveredDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    public double Amount { get; private set; }

    public MaterialSupply(int materialId, DateTime deliveredDate, decimal totalPrice, double amount)
    {
        MaterialId = materialId;
        DeliveredDate = deliveredDate;
        TotalPrice = totalPrice;
        Amount = amount;
    }

    public void UpdateDetails(MaterialSupplyDetails details)
    {
        //Guard.Against.Zero(details.MaterialId, nameof(details.MaterialId));
        Guard.Against.Default(details.DeliveredDate, nameof(details.DeliveredDate));
        Guard.Against.NegativeOrZero(details.TotalPrice, nameof(details.TotalPrice));
        Guard.Against.NegativeOrZero(details.Amount, nameof(details.Amount));


        DeliveredDate = details.DeliveredDate;
        TotalPrice = details.TotalPrice;
        Amount = details.Amount;


    }
    
    public readonly record struct MaterialSupplyDetails
    {
        public DateTime DeliveredDate { get; }
        public decimal TotalPrice { get; }
        public double Amount { get; }

        public MaterialSupplyDetails(DateTime deliveredDate, decimal totalPrice, double amount)
        {
            DeliveredDate = deliveredDate;
            TotalPrice = totalPrice;
            Amount = amount;
        }
    }
}
