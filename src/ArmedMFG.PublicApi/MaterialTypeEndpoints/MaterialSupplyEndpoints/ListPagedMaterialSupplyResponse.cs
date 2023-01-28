using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class ListPagedMaterialSupplyResponse : BaseResponse
{
    public ListPagedMaterialSupplyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedMaterialSupplyResponse()
    {
    }

    public List<MaterialSupplyDto> MaterialSupplies { get; set; } = new List<MaterialSupplyDto>();
    public int PageCount {get; set; }
}
