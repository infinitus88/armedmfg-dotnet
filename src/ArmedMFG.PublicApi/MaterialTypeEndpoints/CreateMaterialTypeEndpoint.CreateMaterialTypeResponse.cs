using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class CreateMaterialTypeResponse : BaseResponse
{
    public CreateMaterialTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateMaterialTypeResponse()
    {
    }

    [JsonPropertyName("material")]
    public MaterialTypeDto MaterialType { get; set; }
}
