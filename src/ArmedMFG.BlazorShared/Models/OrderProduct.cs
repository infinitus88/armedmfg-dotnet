using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class OrderProduct
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductType { get; set; } = "NotSet";
    
    [Range(1, 10000)]
    public int Quantity { get; set; }
    
    public bool HaveSingleTimePrice { get; set; }
    
    public decimal SingleTimePrice { get; set; }
}
