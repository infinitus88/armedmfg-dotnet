using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class UpdateProductMaterialRequest : BaseRequest
{
    [Required]
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public double Amount { get; set; }
}
