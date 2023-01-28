using System;
using ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class CreateMaterialSupplyResponse : BaseResponse
{
    public CreateMaterialSupplyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateMaterialSupplyResponse()
    {
    }

    public MaterialSupplyDto MaterialSupply { get; set; }
}
