namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class ListPagedMaterialSupplyRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? MaterialTypeId { get; set; }

    public ListPagedMaterialSupplyRequest(int? pageSize, int? pageIndex, int? materialTypeId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        MaterialTypeId = materialTypeId;
    }
}
