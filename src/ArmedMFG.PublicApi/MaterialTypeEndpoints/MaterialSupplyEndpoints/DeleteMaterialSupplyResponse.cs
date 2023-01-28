using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class DeleteMaterialSupplyResponse : BaseResponse
{
    public DeleteMaterialSupplyResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteMaterialSupplyResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
