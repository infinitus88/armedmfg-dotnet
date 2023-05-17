using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

public class SearchCustomerRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchCustomerFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public SearchCustomerRequest(SearchCustomerFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchCustomerFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchCustomerFilter
{
    public string? SearchText { get; set; } = string.Empty;
}
