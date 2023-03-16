using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class FindListPagedOrderRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindOrderFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedOrderRequest(FindOrderFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindOrderFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindOrderFilter
{
    [JsonPropertyName("name")]
    public string? Name { get; set; } = String.Empty;
    public string? Id { get; set; } = String.Empty;
    public string? CustomerName { get; set; } = String.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
