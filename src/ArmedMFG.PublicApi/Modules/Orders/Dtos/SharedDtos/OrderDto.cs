﻿using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.OrderShipments.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

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
    public List<OrderProductForOrderDto> OrderProducts { get; set; } = new List<OrderProductForOrderDto>();
    public List<OrderShipmentDto> OrderShipments { get; set; } = new List<OrderShipmentDto>();
}