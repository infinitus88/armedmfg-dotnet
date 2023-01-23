using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class GetByIdMaterialTypeResponse : BaseResponse
{
    public GetByIdMaterialTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdMaterialTypeResponse()
    {
    }

    public MaterialTypeDto MaterialType { get; set; }
}
