using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class SearchPaymentRecordsRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchPaymentRecordsFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public SearchPaymentRecordsRequest(SearchPaymentRecordsFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchPaymentRecordsFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchPaymentRecordsFilter
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int? PaymentCategoryId { get; set; }
    public int? PaymentType { get; set; }
}
