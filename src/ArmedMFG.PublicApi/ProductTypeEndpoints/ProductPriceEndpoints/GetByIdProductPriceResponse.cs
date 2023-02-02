using System;
using ArmedMFG.PublicApi.MaterialTypeEndpoints;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class GetByIdProductPriceResponse : BaseResponse
{
    public GetByIdProductPriceResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdProductPriceResponse()
    {
    }

    public ProductPriceDto ProductPrice { get; set; }
}
