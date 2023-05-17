﻿namespace ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

public class OrderProductForOrderDto
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public string? ProductTypeName { get; set; }
    public int Quantity { get; set; }
    public int ShippedQuantity { get; set; }
    public decimal Price { get; set; }
}

public class OrderProductInfoDto
{
    public int Id { get; set; }
    public string ProductTypeName { get; set; }
    public int Quantity { get; set; }
}