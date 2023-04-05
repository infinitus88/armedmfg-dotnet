using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints.MaterialCheckPointEndpoints;

public class FindListPagedMaterialCheckPointRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindMaterialCheckPointFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedMaterialCheckPointRequest(FindMaterialCheckPointFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindMaterialCheckPointFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindMaterialCheckPointFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Name { get; set; } = String.Empty;
    public int MaterialCategoryId { get; set; }
}
