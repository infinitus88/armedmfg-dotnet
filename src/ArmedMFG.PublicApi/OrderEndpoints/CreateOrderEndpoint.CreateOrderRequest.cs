using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class CreateOrderRequest : BaseRequest
{
    public int CustomerId { get; set; }
    public string OrderedDate { get; set; }
    public string RequiredDate { get; set; }
    public decimal TotalAmount { get; set; }
    // public string Description { get; set; }

    public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
}
