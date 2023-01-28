using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class UpdateProductPriceRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    
    [Range(1, 10000)]
    public int ProductTypeId { get; set; }
    
    [Required]
    public DateTime FromDate { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}
