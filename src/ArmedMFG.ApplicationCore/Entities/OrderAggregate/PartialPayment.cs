using System;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class PartialPayment : BaseEntity, IAggregateRoot
{
    public int OrderId { get; private set; }
    public Order Order { get; private set; }
    public DateTime PayedDate { get; private set; }
    public decimal Amount { get; private set; }

    public PartialPayment(int orderId, DateTime payedDate, decimal amount)
    {
        OrderId = orderId;
        PayedDate = payedDate;
        Amount = amount;
    }
}
