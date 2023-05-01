using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.OrderEndpoints;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class SearchProductBatchResponse : BaseResponse
{
    public SearchProductBatchResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchProductBatchResponse()
    {
    }

    public List<ProductBatchInfoDto> ProductBatches { get; set; } = new List<ProductBatchInfoDto>();
    public int TotalCount { get; set; }
}
