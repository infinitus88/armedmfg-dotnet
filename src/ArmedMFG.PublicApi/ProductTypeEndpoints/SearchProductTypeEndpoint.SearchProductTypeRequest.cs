using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class SearchProductTypeRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchProductTypeFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public SearchProductTypeRequest(SearchProductTypeFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchProductTypeFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchProductTypeFilter
{
    public string? SearchText { get; set; } = String.Empty;
    public int ProductCategoryId { get; set; }
}
