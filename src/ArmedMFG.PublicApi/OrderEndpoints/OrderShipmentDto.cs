using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class OrderShipmentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string CustomerFullName { get; set; } = String.Empty;
    public string ShipmentDate { get; set; } 
    public string DriverName { get; set; } = String.Empty;
    public string DriverPhone { get; set; } = String.Empty;
    public string CarNumber { get; set; } = String.Empty;
    public string Destination { get; set; } = String.Empty;
    public List<ShipmentProductDto> ShipmentProducts { get; set; } = new List<ShipmentProductDto>();
}

public class OrderShipmentInfoDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string? CustomerFullName { get; set; }
    public string? DriverName { get; set; }
    public string? DriverPhone { get; set; }
    public string? CarNumber { get; set; }
    public string? Destination { get; set; }
    public DateTime ShipmentDate { get; set; } 
}
