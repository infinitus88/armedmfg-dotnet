using System;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.WarehouseAggregate;

public class WarehouseProductCheckPoint : BaseEntity, IAggregateRoot
{
    public DateTime CheckedDate { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public int Quantity  { get; set; }
}
