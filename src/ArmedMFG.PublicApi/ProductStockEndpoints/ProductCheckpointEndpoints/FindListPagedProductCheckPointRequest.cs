using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

public class FindListPagedProductCheckPointRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindProductCheckPointFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedProductCheckPointRequest(FindProductCheckPointFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindProductCheckPointFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindProductCheckPointFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Name { get; set; } = String.Empty;
    public int ProductCategoryId { get; set; }
}
