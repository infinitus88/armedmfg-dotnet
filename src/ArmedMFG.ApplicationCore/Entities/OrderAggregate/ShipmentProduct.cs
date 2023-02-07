using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class ShipmentProduct : BaseEntity, IAggregateRoot
{
    public int PartialShipmentId { get; private set; }
    public PartialShipment PartialShipment { get; private set; }
    public int ProductTypeId { get; private set; }
    public ProductType ProductType { get; private set; }
    public int Quantity { get; private set; }

    public ShipmentProduct(int partialShipmentId, int productTypeId, int quantity)
    {
        PartialShipmentId = partialShipmentId;
        ProductTypeId = productTypeId;
        Quantity = quantity;
    }
}
