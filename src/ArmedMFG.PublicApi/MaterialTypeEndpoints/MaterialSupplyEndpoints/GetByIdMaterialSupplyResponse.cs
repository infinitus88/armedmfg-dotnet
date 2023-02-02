using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class GetByIdMaterialSupplyResponse : BaseResponse
{
    public GetByIdMaterialSupplyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdMaterialSupplyResponse()
    {
    }

    public MaterialSupplyDto MaterialSupply { get; set; }
}
