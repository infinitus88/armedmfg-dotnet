using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints.MaterialCheckPointEndpoints;

public class FindListPagedMaterialCheckPointResponse : BaseResponse
{
    public FindListPagedMaterialCheckPointResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedMaterialCheckPointResponse()
    {
    }

    public List<MaterialCheckPointDto> MaterialCheckPoints { get; set; } = new List<MaterialCheckPointDto>();
    public int TotalCount { get; set; }
}
