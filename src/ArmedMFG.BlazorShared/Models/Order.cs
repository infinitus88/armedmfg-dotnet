using System;
using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderedDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public int CustomerId { get; set; }
    public string Customer { get; set; } = "NotSet";
    public byte Status { get; set; }
    public byte PaymentType { get; set; }
    public string Description { get; set; }
    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public List<OrderShipment> OrderShipments { get; set; } = new List<OrderShipment>();
}
