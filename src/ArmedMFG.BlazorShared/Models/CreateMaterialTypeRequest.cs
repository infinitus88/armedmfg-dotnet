using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateMaterialTypeRequest
{
    public int MaterialCategoryId { get; set; }
    
    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; } = string.Empty;
}
