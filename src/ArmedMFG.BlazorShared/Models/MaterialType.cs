using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using ArmedMFG.BlazorShared.Attributes;

namespace ArmedMFG.BlazorShared.Models;

[Endpoint(Name = "material-types")]
public class MaterialType
{
    public int Id { get;set; }
    public int MaterialCategoryId { get; set; }
    public string MaterialCategory { get; set; } = "NotSet";
    
    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; } 
}
