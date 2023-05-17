using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class SearchMaterialSupplyResponse : BaseResponse
{
    public SearchMaterialSupplyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchMaterialSupplyResponse()
    {
    }

    public List<MaterialSupplyDto> MaterialSupplies { get; set; } = new List<MaterialSupplyDto>();
    public int TotalCount { get; set; }
}
