using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class CreateProducedProductRequest : BaseRequest
{
    [Required]
    public int ProductionReportId { get; set; }

    [Required]
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
