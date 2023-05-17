using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos.SharedDtos;

public class OrderShipmentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string CustomerFullName { get; set; } = string.Empty;
    public string ShipmentDate { get; set; }
    public string DriverName { get; set; } = string.Empty;
    public string DriverPhone { get; set; } = string.Empty;
    public string CarNumber { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
}
