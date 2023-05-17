using System;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class UpdateMaterialSupplyResponse : BaseResponse
{
    public UpdateMaterialSupplyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateMaterialSupplyResponse()
    {
    }

    public MaterialSupplyDto? MaterialSupply { get; set; }
}
