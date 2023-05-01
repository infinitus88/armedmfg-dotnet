using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.ProductBatchEndpoints;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class SearchProductTypeResponse : BaseResponse
{
    public SearchProductTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchProductTypeResponse()
    {
    }

    public List<ProductTypeInfoDto> ProductTypes { get; set; } = new List<ProductTypeInfoDto>();
    public int TotalCount { get; set; }
}
