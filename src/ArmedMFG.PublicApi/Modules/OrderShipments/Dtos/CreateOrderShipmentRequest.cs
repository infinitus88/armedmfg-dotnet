using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;

public class CreateOrderShipmentRequest : BaseRequest
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public string ShipmentDate { get; set; }
    public string DriverName { get; set; }
    public string DriverPhone { get; set; }
    public string CarNumber { get; set; }
    public string Destination { get; set; }

    public List<OrderProductForCreationDto> ShipmentProducts { get; set; } = new List<OrderProductForCreationDto>();
}

public class OrderProductForCreationDto
{
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
}

