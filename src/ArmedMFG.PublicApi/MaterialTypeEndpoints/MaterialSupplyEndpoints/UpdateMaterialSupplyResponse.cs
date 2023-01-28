using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class UpdateMaterialSupplyResponse : BaseResponse
{
    public UpdateMaterialSupplyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateMaterialSupplyResponse()
    {
    }
    
    public MaterialSupplyDto MaterialSupply { get; set; }
}
