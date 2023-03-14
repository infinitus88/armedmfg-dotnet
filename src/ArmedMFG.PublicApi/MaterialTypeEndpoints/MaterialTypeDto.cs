using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class MaterialTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaterialCategoryId { get; set; }
}
