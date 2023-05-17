using System;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class UpdateMaterialResponse : BaseResponse
{
    public UpdateMaterialResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateMaterialResponse()
    {
    }

    public MaterialDto? Material { get; set; }
}
