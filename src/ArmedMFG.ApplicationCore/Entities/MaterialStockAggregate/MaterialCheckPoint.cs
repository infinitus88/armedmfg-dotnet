using System;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;

public class MaterialCheckPoint : BaseEntity, IAggregateRoot
{
    public DateTime CheckedDate { get; set; }
    public int MaterialTypeId { get; set; }
    public MaterialType? MaterialType { get; set; }
    public double Amount { get; set; }

    public MaterialCheckPoint(DateTime checkedDate, int materialTypeId, double amount)
    {
        CheckedDate = checkedDate;
        MaterialTypeId = materialTypeId;
        Amount = amount;
    }
}
