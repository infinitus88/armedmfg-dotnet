using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;

public class SearchOrderShipmentRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchOrderShipmentFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchOrderShipmentRequest(SearchOrderShipmentFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchOrderShipmentFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchOrderShipmentFilter
{
    public string? SearchText { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}
