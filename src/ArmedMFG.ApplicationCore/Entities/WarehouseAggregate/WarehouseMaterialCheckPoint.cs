using System;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.WarehouseAggregate;

public class WarehouseMaterialCheckPoint : BaseEntity, IAggregateRoot
{
    public DateTime CheckedDate { get; set; }
    public int MaterialTypeId { get; set; }
    public MaterialType MaterialType { get; set; }
    public double Amount { get; set; }
}
