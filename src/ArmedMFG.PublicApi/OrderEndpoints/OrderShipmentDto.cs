﻿using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class OrderShipmentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string DriverName { get; set; }
    public string DriverPhone { get; set; }
    public string CarNumber { get; set; }
    public List<ShipmentProductDto> ShipmentProducts { get; set; } = new List<ShipmentProductDto>();
    public DateTime ShipmentDate { get; set; } 
}
