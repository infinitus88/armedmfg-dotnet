using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.ProductStockEndpoints;

public class FindListPagedProductStockInfoRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindProductStockInfoFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedProductStockInfoRequest(FindProductStockInfoFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindProductStockInfoFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindProductStockInfoFilter
{
    public string? Name { get; set; } = String.Empty;
    public int ProductCategoryId { get; set; }
}
