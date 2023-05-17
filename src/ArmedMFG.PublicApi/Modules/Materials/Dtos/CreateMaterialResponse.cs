using System;
using System.Text.Json.Serialization;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class CreateMaterialResponse : BaseResponse
{
    public CreateMaterialResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateMaterialResponse()
    {
    }

    [JsonPropertyName("material")]
    public MaterialDto Material { get; set; }
}
