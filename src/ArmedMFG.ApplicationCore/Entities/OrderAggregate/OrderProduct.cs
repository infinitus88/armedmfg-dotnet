using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class OrderProduct : BaseEntity, IAggregateRoot
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public OrderProduct(int productTypeId, int quantity, decimal price)
    {
        ProductTypeId = productTypeId;
        Quantity = quantity;
        Price = price;
    }
}
