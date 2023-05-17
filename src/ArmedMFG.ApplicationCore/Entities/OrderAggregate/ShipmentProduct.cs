using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class ShipmentProduct : BaseEntity, IAggregateRoot
{
    public int OrderShipmentId { get; private set; }
    public OrderShipment OrderShipment { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    public ShipmentProduct(int orderShipmentId, int productId, int quantity)
    {
        OrderShipmentId = orderShipmentId;
        ProductId = productId;
        Quantity = quantity;
    }
}
