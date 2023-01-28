using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class CreateProductPriceResponse : BaseResponse
{
    public CreateProductPriceResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProductPriceResponse()
    {
    }

    public ProductPriceDto ProductPrice { get; set; }
}
