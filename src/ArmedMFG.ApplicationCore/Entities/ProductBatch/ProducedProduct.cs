using Ardalis.GuardClauses;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.ApplicationCore.Entities.ProductBatch;

public class ProducedProduct : BaseEntity
{
    public int ProductTypeId { get; private set; }
    public ProductType? ProductType { get; private set; }
    public int ProductBatchId { get; private set; }
    public ProductBatch? ProductBatch { get; private set; }
    public int Quantity { get; private set; }

    public ProducedProduct(int productTypeId,
        int quantity)
    {
        ProductTypeId = productTypeId;
        SetQuantity(quantity);
    }

    public ProducedProduct(int productTypeId,
        int productBatchId,
        int quantity)
    {
        ProductTypeId = productTypeId;
        ProductBatchId = productBatchId;
        SetQuantity(quantity);
    }

    public void AddQuantity(int quantity)
    {
        Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

        Quantity = quantity;
    }

    public void UpdateDetails(ProducedProductDetails details)
    {
        Guard.Against.NegativeOrZero(details.ProductTypeId, nameof(details.ProductTypeId));
        Guard.Against.NegativeOrZero(details.Quantity, nameof(details.Quantity));

        ProductTypeId = details.ProductTypeId;
        Quantity = details.Quantity;
    }

    public readonly record struct ProducedProductDetails
    {
        public int ProductTypeId { get; init; }
        public int Quantity { get; init; }

        public ProducedProductDetails( int productTypeId, int quantity)
        {
            ProductTypeId = productTypeId;
            Quantity = quantity;
        }
    }
}
