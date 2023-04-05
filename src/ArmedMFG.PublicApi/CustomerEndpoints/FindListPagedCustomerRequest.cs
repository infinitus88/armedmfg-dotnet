using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class FindListPagedCustomerRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindOCustomerFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedCustomerRequest(FindOCustomerFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindOCustomerFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindOCustomerFilter
{
    public string? FullName { get; set; } = String.Empty;
}
