using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class SearchProductRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchProductFilter Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchProductRequest(SearchProductFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchProductFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchProductFilter
{
    public string SearchText { get; set; } = string.Empty;
    public int ProductCategoryId { get; set; }
}
