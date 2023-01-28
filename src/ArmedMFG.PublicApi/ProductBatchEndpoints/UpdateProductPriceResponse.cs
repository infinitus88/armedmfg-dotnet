using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class UpdateProductPriceResponse : BaseResponse
{
    public UpdateProductPriceResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateProductPriceResponse()
    {
    }
    
    public ProductPriceDto ProductPrice { get; set; }
}
