using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints;

public class FindListPagedMaterialStockInfoResponse : BaseResponse
{
    public FindListPagedMaterialStockInfoResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedMaterialStockInfoResponse()
    {
    }

    public List<MaterialStockInfoDto> MaterialStocks { get; set; } = new List<MaterialStockInfoDto>();
}
