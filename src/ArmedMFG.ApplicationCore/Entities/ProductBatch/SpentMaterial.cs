using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Entities.ProductBatch;

public class SpentMaterial : BaseEntity
{
    public int MaterialTypeId { get; private set; }
    public MaterialType? MaterialType { get; set; }
    public int ProductBatchId { get; private set; }
    public ProductBatch? ProductBatch { get; private set; }
    public float Amount { get; private set; }

    public SpentMaterial(
        int materialTypeId,
        int productBatchId,
        float amount)
    {
        MaterialTypeId = materialTypeId;
        ProductBatchId = productBatchId;
        SetAmount(amount);
    }

    public SpentMaterial(
        int materialTypeId,
        float amount)
    {
        MaterialTypeId = materialTypeId;
        SetAmount(amount);
    }

    public void AddQuantity(float amount)
    {
        Guard.Against.OutOfRange(amount, nameof(amount), 0, float.MaxValue);

        Amount += amount;
    }

    public void SetAmount(float amount)
    {
        Guard.Against.OutOfRange(amount, nameof(amount), 0, float.MaxValue);

        Amount = amount;
    } 
}
