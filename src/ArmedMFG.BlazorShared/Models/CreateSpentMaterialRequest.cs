using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateSpentMaterialRequest
{
    [Required(ErrorMessage = "The MaterialType field is required")]
    public int MaterialTypeId { get; set; }
    
    [Required(ErrorMessage = "The field Amount must be a positive number with maximum two decimals.")]
    [Range(1.00, 1000000.00)] 
    public double Amount { get; set; }
}
