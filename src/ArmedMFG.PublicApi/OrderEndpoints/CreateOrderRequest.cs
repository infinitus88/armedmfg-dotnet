using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class CreateOrderRequest : BaseRequest
{
    public int CustomerId { get; set; }
    public DateTime OrderedDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public byte PaymentType { get; set; }
    public decimal TotalAmount { get; set; }
    public string Description { get; set; }

    public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
}
