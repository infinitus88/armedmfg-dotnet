using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateOrderProductRequest
{
    [Required(ErrorMessage = "The ProductType field is required")]
    public int ProductTypeId { get; set; }
    
    [Range(1, 10000)]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = "The HaveSingleTimePrice field is required")]
    public bool HaveSingleTimePrice { get; set; }
    
    public decimal SingleTimePrice { get; set; }
}
