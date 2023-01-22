using System.Collections.Generic;
using System.Text.Json.Serialization;
using ArmedMFG.BlazorShared.Interfaces;

namespace ArmedMFG.BlazorShared.Models;

public class CatalogTypeResponse : ILookupDataResponse<CatalogType>
{

    [JsonPropertyName("CatalogTypes")]
    public List<CatalogType> List { get; set; } = new List<CatalogType>();
}
