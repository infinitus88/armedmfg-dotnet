using System;
using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class SearchProducedProductRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchProducedProductFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchProducedProductRequest(SearchProducedProductFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchProducedProductFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchProducedProductFilter
{
    public string? SearchText { get; set; } = string.Empty;
    [Required]
    public int ProductionReportId { get; set; }
}
