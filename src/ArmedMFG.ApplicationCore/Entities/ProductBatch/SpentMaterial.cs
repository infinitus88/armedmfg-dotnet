using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductBatch;

public class SpentMaterial : BaseEntity, IAggregateRoot
{
    public int MaterialTypeId { get; private set; }
    public MaterialType? MaterialType { get; set; }
    public int ProductBatchId { get; private set; }
    public ProductBatch? ProductBatch { get; private set; }
    public double Amount { get; private set; }

    public SpentMaterial(
        int materialTypeId,
        int productBatchId,
        double amount)
    {
        MaterialTypeId = materialTypeId;
        ProductBatchId = productBatchId;
        SetAmount(amount);
    }

    public SpentMaterial(
        int materialTypeId,
        double amount)
    {
        MaterialTypeId = materialTypeId;
        SetAmount(amount);
    }

    public void AddQuantity(double amount)
    {
        Guard.Against.OutOfRange(amount, nameof(amount), 0, double.MaxValue);
        Guard.Against.Negative(amount, nameof(amount));

        Amount += amount;
    }

    public void SetAmount(double amount)
    {
        Guard.Against.OutOfRange(amount, nameof(amount), 0, double.MaxValue);
        Guard.Against.Negative(amount, nameof(amount));

        Amount = amount;
    }

    public void UpdateDetails(SpentMaterialDetails details)
    {
        Guard.Against.NegativeOrZero(details.MaterialTypeId, nameof(details.MaterialTypeId));
        Guard.Against.NegativeOrZero(details.Amount, nameof(details.Amount));

        MaterialTypeId = details.MaterialTypeId;
        Amount = details.Amount;
    }

    public readonly record struct SpentMaterialDetails
    {
        public int MaterialTypeId { get; init; }
        public double Amount { get; init; }

        public SpentMaterialDetails(int materialTypeId, double amount)
        {
            MaterialTypeId = materialTypeId;
            Amount = amount;
        }
    }
    
}
