using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class CreateMaterialTypeResponse : BaseResponse
{
    public CreateMaterialTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateMaterialTypeResponse()
    {
    }

    public MaterialTypeDto MaterialType { get; set; }
}
