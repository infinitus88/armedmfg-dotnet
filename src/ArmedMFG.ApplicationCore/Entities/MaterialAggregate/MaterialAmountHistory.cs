using System;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

public class MaterialAmountHistory : BaseEntity, IAggregateRoot
{
    public DateTime ChangeDate { get; set; }
    public int MaterialId { get; private set; }
    public Material? Material { get; private set; }
    public double FromAmount { get; private set; }
    public double ToAmount { get; private set; }
    public MaterialAmountChangeType ChangeType { get; private set; }

    public MaterialAmountHistory(
        DateTime changeDate, 
        int materialId,
        double fromAmount,
        double toAmount,
        MaterialAmountChangeType changeType)
    {
        ChangeDate = changeDate;
        MaterialId = materialId;
        FromAmount = fromAmount;;
        ToAmount = toAmount;
        ChangeType = changeType;
    }
}

public enum MaterialAmountChangeType : byte
{
    ManuallyChanged = 0,
    UsedForProduct = 1,
    ThrownOut = 2,
    Delivered = 3
}
