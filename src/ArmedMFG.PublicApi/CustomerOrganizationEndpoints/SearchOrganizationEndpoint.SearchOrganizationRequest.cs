using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints
{
    public class SearchOrganizationRequest : BaseRequest
    {
        [JsonPropertyName("filter")]
        public SearchOrganizationFilter? Filter { get; set; }
        public string? SortOrder { get; set; }
        public string? SortField { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

        public SearchOrganizationRequest(SearchOrganizationFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
        {
            Filter = filter ?? new SearchOrganizationFilter();
            SortOrder = sortOrder;
            SortField = sortField;
            PageSize = pageSize ?? 1;
            PageNumber = pageNumber ?? 1;
        }
    }

    public class SearchOrganizationFilter
    {
        public string? SearchText { get; set; } = String.Empty;
    }
}
