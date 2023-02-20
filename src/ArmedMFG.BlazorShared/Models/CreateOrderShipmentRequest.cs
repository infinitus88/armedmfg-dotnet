using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateOrderShipmentRequest
{
    [Required(ErrorMessage = "The OrderId field is required")]
    public int OrderId { get; set; }
    public string DriverName { get; set; }
    public string DriverPhone { get; set; }
    public string CarNumber { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string Destination { get; set; }
    public List<CreateShipmentProduct> ShipmentProducts { get; set; } = new List<CreateShipmentProduct>();
}
