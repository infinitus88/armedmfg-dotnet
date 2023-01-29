using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class UpdateProducedProductRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int ProductTypeId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}
