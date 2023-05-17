using System;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class GetMaterialSupplyForEditResponse : BaseResponse
{
    public GetMaterialSupplyForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetMaterialSupplyForEditResponse()
    {
    }

    public MaterialSupplyForEditDto? MaterialSupplyForEdit { get; set; }
}
