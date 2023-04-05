using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductStockEndpoints;

public class FindListPagedProductStockInfoResponse : BaseResponse
{
    public FindListPagedProductStockInfoResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedProductStockInfoResponse()
    {
    }

    public List<ProductStockInfoDto> ProductStocks { get; set; } = new List<ProductStockInfoDto>();
}
