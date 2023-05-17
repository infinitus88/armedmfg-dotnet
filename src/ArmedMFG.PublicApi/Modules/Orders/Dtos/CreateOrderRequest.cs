using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class CreateOrderRequest : BaseRequest
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public string OrderedDate { get; set; }
    [Required]
    public string RequiredDate { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }
    public string Description { get; set; } 
    public List<OrderProductForCreation> OrderProducts { get; set; } = new List<OrderProductForCreation>();
}

public class OrderProductForCreation
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
