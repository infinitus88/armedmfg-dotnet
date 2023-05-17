using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class UpdateProducedProductRequest : BaseRequest
{
    [Required]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
