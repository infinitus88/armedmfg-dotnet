using System;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class PartialPayment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime PayedDate { get; set; }
    public decimal Amount { get; set; }

    public PartialPayment(int orderId, DateTime payedDate, decimal amount)
    {
        OrderId = orderId;
        PayedDate = payedDate;
        Amount = amount;
    }
}
