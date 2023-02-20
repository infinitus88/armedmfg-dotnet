using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedOrderShipmentResponse
{
    public List<OrderShipment> OrderShipments { get; set; } = new List<OrderShipment>();
}
