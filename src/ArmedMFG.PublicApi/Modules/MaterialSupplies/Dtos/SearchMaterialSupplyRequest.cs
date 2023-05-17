using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class SearchMaterialSupplyRequest : BaseRequest
{
    [JsonPropertyName("filter")]
    public SearchMaterialSupplyFilter? Filter { get; set; }
    public string? SortOrder { get; set; }
    public string? SortField { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public SearchMaterialSupplyRequest(SearchMaterialSupplyFilter? filter, string? sortField, string? sortOrder, int? pageSize, int? pageNumber)
    {
        Filter = filter ?? new SearchMaterialSupplyFilter();
        SortOrder = sortOrder;
        SortField = sortField;
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 1;
    }
}

public class SearchMaterialSupplyFilter
{
    public string? SearchText { get; set; } = string.Empty;
    public int MaterialId { get; set; }
}
