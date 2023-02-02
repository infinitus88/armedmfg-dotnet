using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class Order : BaseEntity, IAggregateRoot
{
    public DateTime OrderedDate { get; private set; }
    public DateTime RequiredDate { get; private set; }
    public DateTime ShippedDate { get; private set; }
    public int ClientId { get; private set; }
    public Customer? Customer { get; private set; }
    public Status Status { get; private set; }
    public string Description { get; private set; }

    private readonly List<PartialPayment> _partialPayments = new List<PartialPayment>();
    public IReadOnlyCollection<PartialPayment> PartialPayments => _partialPayments.AsReadOnly();

    private readonly List<OrderProduct> _orderProducts = new List<OrderProduct>();
    public IReadOnlyCollection<OrderProduct> OrderProducts => _orderProducts.AsReadOnly();

    public Order(DateTime orderedDate,
        int clientId,
        string description
        )
    {
        OrderedDate = orderedDate;
        ClientId = clientId;
        Description = description;
    }

    public void AddRangePartialPayments(IEnumerable<PartialPayment> partialPayments)
    {
        foreach (var partialPayment in partialPayments)
        {
            _partialPayments.Add(partialPayment);
        }
    }

    public void AddRangeProducts(IEnumerable<OrderProduct> orderProducts)
    {
        foreach (var orderProduct in orderProducts) 
        {
            _orderProducts.Add(orderProduct);
        } 
    }

    public void UpdateDetails(OrderDetails details)
    {
        Guard.Against.Default(details.OrderedDate, nameof(details.OrderedDate));
        Guard.Against.Zero(details.ClientId, nameof(details.ClientId));

        OrderedDate = details.OrderedDate;
        ClientId = details.ClientId;
    }

    public readonly record struct OrderDetails
    {
        public DateTime OrderedDate { get; }
        public int ClientId { get; }

        public OrderDetails(DateTime orderedDate, int clientId)
        {
            OrderedDate = orderedDate;
            ClientId = clientId;
        }
    }
}

public enum PaymentType : byte
{
    Transfer = 1,
    CashWithVAT = 2,
    CashWithoutVAT = 2
}

public enum Status : byte
{
    Pending = 1,
    InQueue = 2,
    InProcess = 3,
    Canceled = 4,
    WaitsForShipping = 5, 
    Finished = 6,
}
