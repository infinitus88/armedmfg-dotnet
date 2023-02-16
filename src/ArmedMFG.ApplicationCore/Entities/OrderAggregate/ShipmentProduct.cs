using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class ShipmentProduct : BaseEntity, IAggregateRoot
{
    public int OrderShipmentId { get; private set; }
    public OrderShipment OrderShipment { get; private set; }
    public int ProductTypeId { get; private set; }
    public ProductType ProductType { get; private set; }
    public int Quantity { get; private set; }

    public ShipmentProduct(int orderShipmentId, int productTypeId, int quantity)
    {
        OrderShipmentId = orderShipmentId;
        ProductTypeId = productTypeId;
        Quantity = quantity;
    }
}
