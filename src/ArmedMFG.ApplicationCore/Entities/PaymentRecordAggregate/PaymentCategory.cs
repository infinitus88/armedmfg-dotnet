using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

public class PaymentCategory : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public PaymentType Type { get; private set; }

    public PaymentCategory(string name, PaymentType type)
    {
        Name = name;
        Type = type;
    }
}
