using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class FindListPagedProductTypeRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindProductTypeFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedProductTypeRequest(FindProductTypeFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindProductTypeFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindProductTypeFilter
{
    public string? Name { get; set; } = String.Empty;
    public int ProductCategoryId { get; set; }
}
