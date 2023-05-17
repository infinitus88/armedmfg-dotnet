using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class SearchOrderRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchOrderFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchOrderRequest(SearchOrderFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchOrderFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 10;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchOrderFilter
{
    public string? SearchText { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}
