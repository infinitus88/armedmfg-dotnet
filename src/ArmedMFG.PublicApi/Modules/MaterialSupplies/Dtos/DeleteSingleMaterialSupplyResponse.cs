using System;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class DeleteSingleMaterialSupplyResponse : BaseResponse
{
    public DeleteSingleMaterialSupplyResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteSingleMaterialSupplyResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
