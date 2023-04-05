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
    public DateTime? FinishedDate { get; private set; }
    public int CustomerId { get; private set; }
    public Customer? Customer { get; private set; }
    public Status Status { get; private set; }
    public string Description { get; private set; }
    public decimal TotalAmount { get; private set; }
    
    private readonly List<OrderShipment> _orderShipments = new List<OrderShipment>();
    public IReadOnlyCollection<OrderShipment> OrderShipments => _orderShipments.AsReadOnly();

    private readonly List<OrderProduct> _orderProducts = new List<OrderProduct>();
    public IReadOnlyCollection<OrderProduct> OrderProducts => _orderProducts.AsReadOnly();

    public Order(int customerId, 
        DateTime orderedDate,
        DateTime requiredDate,
        decimal totalAmount,
        string description
        )
    {
        CustomerId = customerId;
        OrderedDate = orderedDate;
        RequiredDate = requiredDate;
        TotalAmount = totalAmount;
        Description = description;
    }

    public void AddPartialShipment(DateTime shipmentDate, string driverName, string driverPhone, string carNumber, string destination, IEnumerable<ShipmentProduct> shipmentProducts)
    {
        var partialShipment = new OrderShipment(Id, shipmentDate, driverName, driverPhone, carNumber, destination);

        foreach (var shipmentProduct in shipmentProducts)
        {
            partialShipment.AddShipmentProduct(shipmentProduct.ProductTypeId, shipmentProduct.Quantity);
        }
    }
    
    public void AddPartialShipment(OrderShipment orderShipment)
    {
        _orderShipments.Add(orderShipment);
    }

    public void AddOrderProduct(int productTypeId, int quantity, decimal price)
    {
        _orderProducts.Add(new OrderProduct(productTypeId, quantity, price));
    }

    public void AddRangeProducts(IEnumerable<OrderProduct> orderProducts)
    {
        foreach (var orderProduct in orderProducts) 
        {
            _orderProducts.Add(orderProduct);
        } 
    }

    public void SetStatus(Status status)
    {
        Status = status;
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
        public DateTime RequiredDate { get; }
        public int CustomerId { get; }
        public string Description { get; }

        public OrderDetails(int customerId, DateTime orderedDate, DateTime requiredDate, string description)
        {
            CustomerId = customerId;
            OrderedDate = orderedDate;
            RequiredDate = requiredDate;
            Description = description;
        }
    }
}


public enum Status : byte
{
    Pending = 0,
    InQueue = 1,
    InProcess = 2,
    Canceled = 3,
    WaitsForShipping = 4, 
    Finished = 5,
}
