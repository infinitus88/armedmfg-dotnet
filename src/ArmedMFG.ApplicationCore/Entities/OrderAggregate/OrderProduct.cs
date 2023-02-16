using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class OrderProduct : BaseEntity, IAggregateRoot
{
    public int ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }
    public int Quantity { get; set; }
    public bool HaveSingleTimePrice { get; set; }
    public decimal SingleTimePrice { get; set; }

    public OrderProduct(int productTypeId, int quantity, bool haveSingleTimePrice, decimal singleTimePrice)
    {
        ProductTypeId = productTypeId;
        Quantity = quantity;
        HaveSingleTimePrice = haveSingleTimePrice;
        SingleTimePrice = singleTimePrice;
    }
}
