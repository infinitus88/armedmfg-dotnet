using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class SearchProductionReportRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchProductionReportFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchProductionReportRequest(SearchProductionReportFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchProductionReportFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 10;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchProductionReportFilter
{
    public string? SearchText { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}
