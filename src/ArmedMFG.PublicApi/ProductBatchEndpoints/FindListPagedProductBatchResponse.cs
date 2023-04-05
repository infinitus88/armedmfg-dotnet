using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.OrderEndpoints;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class FindListPagedProductBatchResponse : BaseResponse
{
    public FindListPagedProductBatchResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedProductBatchResponse()
    {
    }

    public List<ProductBatchInfoDto> ProductBatches { get; set; } = new List<ProductBatchInfoDto>();
    public int TotalCount { get; set; }
}
