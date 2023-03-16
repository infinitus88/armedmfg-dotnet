using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class OrderInfoDto
{
    public int Id { get; set; }
    public string? CustomerFullName { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime OrderedDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public byte Status { get; set; }
    public byte PaymentType { get; set; }
    public string? Description { get; set; }
    public List<OrderProductInfoDto> OrderProducts { get; set; } = new List<OrderProductInfoDto>();
    public List<OrderShipmentDto> OrderShipments { get; set; } = new List<OrderShipmentDto>();
}

