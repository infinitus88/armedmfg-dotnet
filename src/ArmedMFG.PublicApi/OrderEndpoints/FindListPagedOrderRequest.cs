using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class FindListPagedOrderRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindOrderTypeFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedOrderRequest(FindOrderTypeFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindOrderTypeFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindOrderTypeFilter
{
    [JsonPropertyName("name")]
    public string? Name { get; set; } = String.Empty;
    public string? Id { get; set; } = String.Empty;
    public string? CategoryId { get; set; } = String.Empty;
}
