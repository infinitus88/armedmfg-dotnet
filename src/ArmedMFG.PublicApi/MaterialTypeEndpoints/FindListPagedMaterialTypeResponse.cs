using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class FindListPagedMaterialTypeResponse : BaseResponse
{
    public FindListPagedMaterialTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedMaterialTypeResponse()
    {
    }

    public List<MaterialTypeDto> MaterialTypes { get; set; } = new List<MaterialTypeDto>();
    public int TotalCount { get; set; }
}
