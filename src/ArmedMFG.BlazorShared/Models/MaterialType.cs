using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace ArmedMFG.BlazorShared.Models;

public class MaterialType
{
    public int Id { get;set; }
    public int MaterialCategoryId { get; set; }
    public string MaterialCategory { get; set; } = "NotSet";
    
    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; } 
    
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Amount must be a positive number with maximum two decimals.")]
    [Range(1.00, 1000000.00)] 
    public decimal CurrentAmount { get; set; } 
}
