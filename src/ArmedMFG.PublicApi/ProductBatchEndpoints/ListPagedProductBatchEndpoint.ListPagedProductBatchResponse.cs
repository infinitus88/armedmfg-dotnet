using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class ListPagedProductBatchResponse : BaseResponse
{
    public ListPagedProductBatchResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedProductBatchResponse()
    {
    }

    public List<ProductBatchDto> ProductBatches { get; set; } = new List<ProductBatchDto>();
    public int PageCount {get; set; }
}
