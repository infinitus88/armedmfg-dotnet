using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class FindListPagedOrderShipmentRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindOrderShipmentFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedOrderShipmentRequest(FindOrderShipmentFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindOrderShipmentFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindOrderShipmentFilter
{
    [JsonPropertyName("name")]
    public string? CustomerName { get; set; } = String.Empty;
    public string? Id { get; set; } = String.Empty;
    public string? DriverName { get; set; } = String.Empty;
    public string? CarNumber { get; set; } = String.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
