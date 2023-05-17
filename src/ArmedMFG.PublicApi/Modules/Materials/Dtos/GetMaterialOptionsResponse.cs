using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class GetMaterialOptionsResponse : BaseResponse
{
    public GetMaterialOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetMaterialOptionsResponse()
    {
    }

    public List<MaterialOptionDto> MaterialOptions { get; set; } = new List<MaterialOptionDto>();
}
