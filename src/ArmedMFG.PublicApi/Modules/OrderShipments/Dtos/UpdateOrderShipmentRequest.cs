using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;

public class UpdateOrderShipmentRequest : BaseRequest
{
    [Required]
    public int Id { get; set; }
    public string ShipmentDate { get; set; }
    public string DriverName { get; set; }
    public string DriverPhone { get; set; }
    public string CarNumber { get; set; }
    public string Destination { get; set; }

    public List<OrderProductForCreationDto> ShipmentProducts { get; set; } = new List<OrderProductForCreationDto>();
}

