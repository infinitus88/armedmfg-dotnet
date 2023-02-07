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
    public int CustomerId { get; private set; }
    public Customer? Customer { get; private set; }
    public Status Status { get; private set; }
    public string Description { get; private set; }

    private readonly List<PartialPayment> _partialPayments = new List<PartialPayment>();
    public IReadOnlyCollection<PartialPayment> PartialPayments => _partialPayments.AsReadOnly();
    
    private readonly List<PartialShipment> _partialShipments = new List<PartialShipment>();
    public IReadOnlyCollection<PartialShipment> PartialShipments => _partialShipments.AsReadOnly();

    private readonly List<OrderProduct> _orderProducts = new List<OrderProduct>();
    public IReadOnlyCollection<OrderProduct> OrderProducts => _orderProducts.AsReadOnly();

    public Order(DateTime orderedDate,
        int customerId,
        string description
        )
    {
        OrderedDate = orderedDate;
        CustomerId = customerId;
        Description = description;
    }

    public void AddRangePartialPayments(IEnumerable<PartialPayment> partialPayments)
    {
        foreach (var partialPayment in partialPayments)
        {
            _partialPayments.Add(partialPayment);
        }
    }

    public void AddPartialPayment(DateTime payedDate, decimal amount)
    {
        _partialPayments.Add(new PartialPayment(Id, payedDate, amount));
    }

    public void AddPartialShipment(DateTime shipmentDate, string driverName, string driverPhone, string carNumber, IEnumerable<ShipmentProduct> shipmentProducts)
    {
        var partialShipment = new PartialShipment(Id, driverName, driverPhone, carNumber, shipmentDate);

        foreach (var shipmentProduct in shipmentProducts)
        {
            partialShipment.AddProducts(shipmentProduct.ProductTypeId, shipmentProduct.Quantity);
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
        Guard.Against.Zero(details.CustomerId, nameof(details.CustomerId));

        OrderedDate = details.OrderedDate; 
        CustomerId = details.CustomerId;
    }

    public readonly record struct OrderDetails
    {
        public DateTime OrderedDate { get; }
        public int CustomerId { get; }

        public OrderDetails(DateTime orderedDate, int customerId)
        {
            OrderedDate = orderedDate;
            CustomerId = customerId;
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
