using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class OrderShipmentDto
{
    public int Id { get; set; }
    public int OrderId { get; private set; }
    public string DriverName { get; private set; }
    public string DriverPhone { get; private set; }
    public string CarNumber { get; private set; }
    public List<ShipmentProductDto> ShipmentProducts { get; set; } = new List<ShipmentProductDto>();
    public DateTime ShipmentDate { get; private set; } 
}
