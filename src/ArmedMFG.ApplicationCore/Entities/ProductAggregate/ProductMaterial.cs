using System;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductAggregate;

public class ProductMaterial : BaseEntity, IAggregateRoot
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int MaterialId { get; set; }
    public Material? Material { get; set; }
    public double Amount { get; set; }

    public ProductMaterial(int productId, int materialId, double amount)
    {
        ProductId = productId;
        MaterialId = materialId;
        Amount = amount;
    }
    
    public ProductMaterial(int materialId, double amount) 
    {
        MaterialId = materialId;
        Amount = amount;
    }
    
    public void UpdateDetails(ProductMaterialDetails details)
    {
        Guard.Against.NegativeOrZero(details.Amount, nameof(details.Amount));

        MaterialId = details.MaterialId;
        Amount = details.Amount;
    }

    public readonly record struct ProductMaterialDetails
    {
        public int MaterialId { get; }
        public double Amount { get; }

        public ProductMaterialDetails(int materialId, double amount)
        {
            MaterialId = materialId;
            Amount = amount;
        }
    }
}
