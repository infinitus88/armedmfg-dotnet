using System;
using System.Collections.Generic;
using System.ComponentModel;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class OrderShipment : BaseEntity, IAggregateRoot
{
    public int OrderId { get; private set; }
    public Order? Order { get; private set; }
    public string DriverName { get; private set; }
    public string DriverPhone { get; private set; }
    public string CarNumber { get; private set; }
    public string Destination { get; private set; }
    public DateTime ShipmentDate { get; private set; }
    
    private readonly List<ShipmentProduct> _shipmentProducts = new List<ShipmentProduct>();
    public IReadOnlyCollection<ShipmentProduct> ShipmentProducts => _shipmentProducts.AsReadOnly();

    public OrderShipment(int orderId, DateTime shipmentDate, string driverName, string driverPhone, string carNumber, string destination)
    {
        OrderId = orderId;
        ShipmentDate = shipmentDate;
        DriverName = driverName;
        DriverPhone = driverPhone;
        CarNumber = carNumber;
        Destination = destination;
    }

    public OrderShipment(DateTime shipmentDate, string driverName, string driverPhone, string carNumber, string destination)
    {
        ShipmentDate = shipmentDate;
        DriverName = driverName;
        DriverPhone = driverPhone;
        CarNumber = carNumber;
        Destination = destination;
    }
    
    public void AddShipmentProduct(int productTypeId, int quantity)
    {
        _shipmentProducts.Add(new ShipmentProduct(Id, productTypeId, quantity));
    }

    public void UpdateDetails(OrderShipmentDetails details)
    {
        Guard.Against.Default(details.ShipmentDate, nameof(details.ShipmentDate));
        Guard.Against.NullOrEmpty(details.DriverName, nameof(details.DriverName));
        Guard.Against.NullOrEmpty(details.DriverPhone, nameof(details.DriverPhone));
        Guard.Against.NullOrEmpty(details.CarNumber, nameof(details.CarNumber));
        Guard.Against.NullOrEmpty(details.Destination, nameof(details.Destination));

        ShipmentDate = details.ShipmentDate;
        DriverName = details.DriverName;
        DriverPhone = details.DriverPhone;
        CarNumber = details.CarNumber;
        Destination = details.Destination;
    }

    public readonly record struct OrderShipmentDetails
    {
        public DateTime ShipmentDate { get; init; }
        public string? DriverName { get; init; }
        public string? DriverPhone { get; init; }
        public string? CarNumber { get; init; }
        public string? Destination { get; init; }

        public OrderShipmentDetails(DateTime shipmentDate, string? driverName, string? driverPhone, string? carNumber, string? destination)
        {
            ShipmentDate = shipmentDate;
            DriverName = driverName;
            DriverPhone = driverPhone;
            CarNumber = carNumber;
            Destination = destination;
        }
    }
}
