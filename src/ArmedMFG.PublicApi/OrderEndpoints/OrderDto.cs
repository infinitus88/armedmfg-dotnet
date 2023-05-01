using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string RequiredDate { get; set; }
    public string OrderedDate { get; set; }
    public string? FinishedDate { get; set; }
    public byte Status { get; set; }
    public string? Description { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    public List<OrderShipmentDto> OrderShipments { get; set; } = new List<OrderShipmentDto>();
}
