using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.EmployeeEndpoints;

public class SearchEmployeesRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchEmployeesFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public SearchEmployeesRequest(SearchEmployeesFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchEmployeesFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 10;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchEmployeesFilter
{
    public string? SearchText { get; set; } = String.Empty;
    public int PositionId { get; set; } = 0;
    public byte Status { get; set; }
}
