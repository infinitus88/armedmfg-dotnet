using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

public class CreateProductCheckPointResponse : BaseResponse
{
    public CreateProductCheckPointResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProductCheckPointResponse()
    {
    }

    [JsonPropertyName("productCheckPoint")]
    public ProductCheckPointDto ProductCheckPoint { get; set; }
}
