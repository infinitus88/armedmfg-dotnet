using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class GetByIdProductTypeResponse : BaseResponse
{
    public GetByIdProductTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdProductTypeResponse()
    {
    }

    public ProductTypeDto ProductType { get; set; }
}
