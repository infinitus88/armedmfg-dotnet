using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class UpdateProductTypeResponse : BaseResponse
{
    public UpdateProductTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateProductTypeResponse()
    {
    }
    
    public ProductTypeDto ProductType { get; set; }
}
