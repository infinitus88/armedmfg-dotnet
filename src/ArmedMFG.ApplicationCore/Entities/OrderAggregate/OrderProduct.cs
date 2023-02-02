using System.Reflection.Metadata;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class OrderProduct : BaseEntity
{
    public int ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }
    public int Quantity { get; set; }
    public bool HaveSingleTimePrice { get; set; }
    public decimal? SingleTimePrice { get; set; }

    public OrderProduct(int productTypeId, int quantity, decimal? singleTimePrice)
    {
        ProductTypeId = productTypeId;
        Quantity = quantity;
        HaveSingleTimePrice = singleTimePrice.HasValue;
        SingleTimePrice = singleTimePrice;
    }
}
