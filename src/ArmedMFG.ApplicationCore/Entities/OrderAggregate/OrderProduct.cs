using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class OrderProduct : BaseEntity, IAggregateRoot
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product? ProductType { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public OrderProduct(int productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
