using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateMaterialTypeRequest
{
    public int MaterialCategoryId { get; set; }
    
    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; } = string.Empty;
    
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Amount must be a positive number with maximum two decimals.")]
    [Range(1.00, 1000000.00)] 
    public decimal CurrentAmount { get; set; } = 0;
}
