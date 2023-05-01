using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class GetProductOptionsResponse : BaseResponse
{
    public GetProductOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetProductOptionsResponse()
    {
    }

    public List<ProductTypeOptionDto> Products { get; set; } = new List<ProductTypeOptionDto>();
}
