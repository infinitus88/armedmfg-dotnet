using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class ShipmentProduct
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductType { get; set; }
    
    [Range(1, 10000)]
    public int Quantity { get; set; }
}
