using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductionReport;

public class ProducedProduct : BaseEntity, IAggregateRoot
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }
    public int ProductionReportId { get; private set; }
    public ProductionReport? ProductionReport { get; private set; }
    public int Quantity { get; private set; }

    public ProducedProduct(int productId,
        int quantity)
    {
        ProductId = productId;
        SetQuantity(quantity);
    }

    public ProducedProduct(int productionReportId,
        int productId,
        int quantity)
    {
        ProductionReportId = productionReportId;
        ProductId = productId;
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
        Guard.Against.NegativeOrZero(details.ProductId, nameof(details.ProductId));
        Guard.Against.NegativeOrZero(details.Quantity, nameof(details.Quantity));

        ProductId = details.ProductId;
        Quantity = details.Quantity;
    }

    public readonly record struct ProducedProductDetails
    {
        public int ProductId { get; init; }
        public int Quantity { get; init; }

        public ProducedProductDetails( int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
