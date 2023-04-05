using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints.MaterialCheckPointEndpoints;

public class CreateMaterialCheckPointResponse : BaseResponse
{
    public CreateMaterialCheckPointResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateMaterialCheckPointResponse()
    {
    }

    [JsonPropertyName("materialCheckPoint")]
    public MaterialCheckPointDto MaterialCheckPoint { get; set; }
}
