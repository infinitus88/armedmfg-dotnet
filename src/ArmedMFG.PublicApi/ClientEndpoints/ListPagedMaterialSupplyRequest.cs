namespace ArmedMFG.PublicApi.ClientEndpoints;

public class ListPagedClientRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? OrganizationId { get; set; }

    public ListPagedClientRequest(int? pageSize, int? pageIndex, int? organizationId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        OrganizationId = organizationId;
    }
}
