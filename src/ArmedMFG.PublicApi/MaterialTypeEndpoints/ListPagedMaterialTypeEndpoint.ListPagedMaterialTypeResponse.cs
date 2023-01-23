using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class ListPagedMaterialTypeResponse : BaseResponse
{
    public ListPagedMaterialTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedMaterialTypeResponse()
    {
    }

    public List<MaterialTypeDto> MaterialTypes { get; set; } = new List<MaterialTypeDto>();
    public int PageCount {get; set; }
}
