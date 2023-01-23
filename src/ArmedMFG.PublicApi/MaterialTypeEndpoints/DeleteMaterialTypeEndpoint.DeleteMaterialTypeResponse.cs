using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class DeleteMaterialTypeResponse : BaseResponse
{
    public DeleteMaterialTypeResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteMaterialTypeResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
