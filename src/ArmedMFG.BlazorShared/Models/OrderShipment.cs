﻿using System;
using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class OrderShipment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string DriverName { get; set; }
    public string DriverPhone { get; set; }
    public string CarNumber { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string Destination { get; set; }
    public List<ShipmentProduct> ShipmentProducts { get; set; } = new List<ShipmentProduct>();
}
