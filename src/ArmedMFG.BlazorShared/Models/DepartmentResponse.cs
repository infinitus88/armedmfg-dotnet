using System.Collections.Generic;
using System.Text.Json.Serialization;
using ArmedMFG.BlazorShared.Interfaces;

namespace ArmedMFG.BlazorShared.Models;

public class DepartmentResponse : ILookupDataResponse<Department>
{
    [JsonPropertyName("Departments")]
    public List<Department> List { get; set; } = new List<Department>();
}

public class ProductCategoryResponse : ILookupDataResponse<ProductCategory>
{
    [JsonPropertyName("ProductCategories")] public List<ProductCategory> List { get; set; } = new List<ProductCategory>();
}

public class MaterialCategoryResponse : ILookupDataResponse<MaterialCategory>
{
    [JsonPropertyName("MaterialCategories")]
    public List<MaterialCategory> List { get; set; } = new List<MaterialCategory>();
}
