using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class CreateProductPriceRequest : BaseRequest
{
    public int ProductTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public decimal Price { get; set; }
}
