using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.ProductTypeEndpoints;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class GetMaterialOptionsResponse : BaseResponse
{
    public GetMaterialOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetMaterialOptionsResponse()
    {
    }

    public List<MaterialTypeOptionDto> Materials { get; set; } = new List<MaterialTypeOptionDto>();
}
