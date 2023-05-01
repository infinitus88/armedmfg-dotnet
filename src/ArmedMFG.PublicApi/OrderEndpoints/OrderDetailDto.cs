using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class OrderDetailDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string RequiredDate { get; set; }
    public string OrderedDate { get; set; }
    public string FinishedDate { get; set; }
    public byte Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Description { get; set; }
    public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    public List<OrderShipmentDto> OrderShipments { get; set; } = new List<OrderShipmentDto>();
    public List<OrderPaymentRecordDto> OrderPaymentRecords { get; set; } = new List<OrderPaymentRecordDto>();
}

public class OrderPaymentRecordDto
{
    public int Id { get; set; }
    public string PayedDate { get; set; }
    public int ReferenceId { get; set; }
    public int Category { get; set; }
    public byte PaymentMethod { get; set; }
    public decimal Amount { get; set; }
}
