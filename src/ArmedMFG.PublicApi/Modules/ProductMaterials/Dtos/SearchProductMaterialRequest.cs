using System;
using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class SearchProductMaterialRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchProductMaterialFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchProductMaterialRequest(SearchProductMaterialFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchProductMaterialFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchProductMaterialFilter
{
    public string? SearchText { get; set; } = string.Empty;
    [Required]
    public int ProductId { get; set; }
}
