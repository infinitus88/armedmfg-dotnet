using System;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class CreateMaterialSupplyResponse : BaseResponse
{
    public CreateMaterialSupplyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateMaterialSupplyResponse()
    {
    }

    public MaterialSupplyDto? MaterialSupply { get; set; }
}
