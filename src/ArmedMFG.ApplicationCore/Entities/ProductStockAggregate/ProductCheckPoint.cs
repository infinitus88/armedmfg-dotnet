using System;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;

public class ProductCheckPoint : BaseEntity, IAggregateRoot
{
    public DateTime CheckedDate { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }
    public int Quantity  { get; set; }

    public ProductCheckPoint(DateTime checkedDate, int productTypeId, int quantity)
    {
        CheckedDate = checkedDate;
        ProductTypeId = productTypeId;
        Quantity = quantity;
    }
}
