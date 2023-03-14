using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class CreateMaterialTypeRequest : BaseRequest
{
    [JsonPropertyName("material")]
    public CreateMaterialTypeData? Data { get; set; }
}

public class CreateMaterialTypeData
{
    public int MaterialCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
