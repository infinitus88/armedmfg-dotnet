using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class UpdateProductRequest : BaseRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int ProductCategoryId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}
