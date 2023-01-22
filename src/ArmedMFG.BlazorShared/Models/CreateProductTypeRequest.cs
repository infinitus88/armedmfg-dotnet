using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateProductTypeRequest
{
    public int ProductCatalogTypeId { get; set; }
    
    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; } = string.Empty;
    
    // decimal(18,2)
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Price must be a positive number with maximum two decimals.")]
    [Range(1000.00, 10000000.00)] 
    [DataType(DataType.Currency)]
    public decimal Price { get; set; } = 0;

    public string PictureUri { get; set; } = string.Empty;
    public string PictureBase64 { get; set; } = string.Empty;
    public string PictureName { get; set; } = string.Empty;
}
