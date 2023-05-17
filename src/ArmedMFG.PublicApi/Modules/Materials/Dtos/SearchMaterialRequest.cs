using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.Materials.Dtos;

public class SearchMaterialRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchMaterialFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchMaterialRequest(SearchMaterialFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchMaterialFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchMaterialFilter
{
    public string? SearchText { get; set; } = string.Empty;
    public int MaterialCategoryId { get; set; }
}
