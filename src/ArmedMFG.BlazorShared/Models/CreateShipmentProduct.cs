using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateShipmentProduct
{
    public int ProductTypeId { get; set; }
    [Range(1, 10000)]
    public int Quantity { get; set; }
}
