using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints;

public class SearchMaterialStockResponse : BaseResponse
{
    public SearchMaterialStockResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchMaterialStockResponse()
    {
    }

    public List<MaterialStockInfoDto> MaterialStocks { get; set; } = new List<MaterialStockInfoDto>();
}
