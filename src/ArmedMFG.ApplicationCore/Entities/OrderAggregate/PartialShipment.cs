using System;
using System.Collections.Generic;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.OrderAggregate;

public class PartialShipment : BaseEntity, IAggregateRoot
{
    public int OrderId { get; private set; }
    public Order Order { get; private set; }
    public string DriverName { get; private set; }
    public string DriverPhone { get; private set; }
    public string CarNumber { get; private set; }
    public DateTime ShipmentDate { get; private set; }
    
    private readonly List<ShipmentProduct> _shipmentProducts = new List<ShipmentProduct>();
    public IReadOnlyCollection<ShipmentProduct> ShipmentProducts => _shipmentProducts.AsReadOnly();

    public PartialShipment(int orderId, string driverName, string driverPhone, string carNumber, DateTime shipmentDate)
    {
        OrderId = orderId;
        DriverName = driverName;
        DriverPhone = driverPhone;
        ShipmentDate = shipmentDate;
    }
    
    public void AddProducts(int productTypeId, int quantity)
    {
        _shipmentProducts.Add(new ShipmentProduct(Id, productTypeId, quantity));
    }
}
