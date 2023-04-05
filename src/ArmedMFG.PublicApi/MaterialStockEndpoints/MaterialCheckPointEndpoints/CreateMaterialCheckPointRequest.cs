using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints.MaterialCheckPointEndpoints;

public class CreateMaterialCheckPointRequest : BaseRequest
{
    [JsonPropertyName("materialCheckPoint")]
    public CreateMaterialCheckPointData? Data { get; set; }
}

public class CreateMaterialCheckPointData
{
    public DateTime CheckedDate { get; set; }
    public int MaterialTypeId { get; set; }
    public double Amount { get; set; }
}
