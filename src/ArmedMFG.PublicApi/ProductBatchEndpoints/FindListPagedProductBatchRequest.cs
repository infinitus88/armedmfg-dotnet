﻿using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class FindListPagedProductBatchRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public FindProductBatchFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }

    public FindListPagedProductBatchRequest(FindProductBatchFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new FindProductBatchFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class FindProductBatchFilter
{
    public string? Name { get; set; } = String.Empty;
    public string? Id { get; set; } = String.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
