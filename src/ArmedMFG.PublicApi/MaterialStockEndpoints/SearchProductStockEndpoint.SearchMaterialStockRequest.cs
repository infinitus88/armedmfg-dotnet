using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints;

public class SearchMaterialStockRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindMaterialStockInfoFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public SearchMaterialStockRequest(FindMaterialStockInfoFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindMaterialStockInfoFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindMaterialStockInfoFilter
{
    public string? SearchText { get; set; } = String.Empty;
    public int ProductCategoryId { get; set; }
}
