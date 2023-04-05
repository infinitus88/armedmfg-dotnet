using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

public class FindListPagedProductCheckPointResponse : BaseResponse
{
    public FindListPagedProductCheckPointResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedProductCheckPointResponse()
    {
    }

    public List<ProductCheckPointDto> ProductCheckPoints { get; set; } = new List<ProductCheckPointDto>();
    public int TotalCount { get; set; }
}
