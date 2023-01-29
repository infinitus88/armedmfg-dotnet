using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Entities.ProductBatch;

public class SpentMaterial : BaseEntity
{
    public int MaterialTypeId { get; private set; }
    public MaterialType? MaterialType { get; set; }
    public int ProductBatchId { get; private set; }
    public ProductBatch? ProductBatch { get; private set; }
    public decimal Amount { get; private set; }

    public SpentMaterial(
        int materialTypeId,
        int productBatchId,
        decimal amount)
    {
        MaterialTypeId = materialTypeId;
        ProductBatchId = productBatchId;
        SetAmount(amount);
    }

    public SpentMaterial(
        int materialTypeId,
        decimal amount)
    {
        MaterialTypeId = materialTypeId;
        SetAmount(amount);
    }

    public void AddQuantity(decimal amount)
    {
        Guard.Against.OutOfRange(amount, nameof(amount), 0, decimal.MaxValue);

        Amount += amount;
    }

    public void SetAmount(decimal amount)
    {
        Guard.Against.OutOfRange(amount, nameof(amount), 0, decimal.MaxValue);

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
        public decimal Amount { get; init; }

        public SpentMaterialDetails(int materialTypeId, decimal amount)
        {
            MaterialTypeId = materialTypeId;
            Amount = amount;
        }
    }
    
}
