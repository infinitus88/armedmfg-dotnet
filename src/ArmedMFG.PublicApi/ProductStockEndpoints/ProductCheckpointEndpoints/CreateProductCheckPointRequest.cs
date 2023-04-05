using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

public class CreateProductCheckPointRequest : BaseRequest
{
    [JsonPropertyName("productCheckPoint")]
    public CreateProductCheckPointData? Data { get; set; }
}

public class CreateProductCheckPointData
{
    public DateTime CheckedDate { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
}
