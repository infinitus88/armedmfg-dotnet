using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class CreateProductTypeResponse : BaseResponse
{
    public CreateProductTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProductTypeResponse()
    {
    }

    public ProductTypeDto ProductType { get; set; }
}
