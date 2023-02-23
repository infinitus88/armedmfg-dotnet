using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class UpdateSpentMaterialRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int MaterialTypeId { get; set; }
    
    [Required]
    public double Amount { get; set; }
}
