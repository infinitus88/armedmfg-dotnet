using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class CreateOrderShipmentRequest : BaseRequest
{
    public int OrderId { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string DriverName { get; set; }
    public string DriverPhone { get; set; }
    public string CarNumber { get; set; }
    public string Destination { get; set; }

    public List<ShipmentProductDto> ShipmentProducts { get; set; } = new List<ShipmentProductDto>();
}
