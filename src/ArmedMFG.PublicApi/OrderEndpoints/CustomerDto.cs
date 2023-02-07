using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public List<PartialPayment> PartialPayments { get; set; } = new List<PartialPayment>();
    public List<PartialShipment> PartialShipments { get; set; } = new List<PartialShipment>();
}

public class PartialShipment
{
    public int Id { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string DriverName { get; set; } = String.Empty;
    public string DriverPhone { get; set; } = String.Empty;
    public List<ShipmentProduct> ShipmentProducts { get; set; } = new List<ShipmentProduct>();
}

public class ShipmentProduct
{
    public int Id { get; set; }
    public int PartialShipmentId { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
}

public class PartialPayment
{
    public int Id { get; set; }
    public DateTime PayedDate { get; set; }
}
