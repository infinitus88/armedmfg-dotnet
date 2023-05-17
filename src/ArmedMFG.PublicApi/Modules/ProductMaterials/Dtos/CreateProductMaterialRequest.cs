using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class CreateProductMaterialRequest : BaseRequest
{
    [Required]
    public int ProductId { get; set; }
    public int MaterialId { get; set; }
    public double Amount { get; set; }
}
