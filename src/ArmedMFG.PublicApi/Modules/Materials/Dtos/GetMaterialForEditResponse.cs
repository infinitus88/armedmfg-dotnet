using System;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class GetMaterialForEditResponse : BaseResponse
{
    public GetMaterialForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetMaterialForEditResponse()
    {
    }

    public MaterialForEditDto? MaterialForEdit { get; set; }
}
