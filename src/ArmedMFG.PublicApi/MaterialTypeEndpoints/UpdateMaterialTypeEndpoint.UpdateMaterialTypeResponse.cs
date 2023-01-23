using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class UpdateMaterialTypeResponse : BaseResponse
{
    public UpdateMaterialTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateMaterialTypeResponse()
    {
    }
    
    public MaterialTypeDto MaterialType { get; set; }
}
