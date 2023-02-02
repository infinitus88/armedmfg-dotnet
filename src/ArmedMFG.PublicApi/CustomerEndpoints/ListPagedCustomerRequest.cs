namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class ListPagedCustomerRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? OrganizationId { get; set; }

    public ListPagedCustomerRequest(int? pageSize, int? pageIndex, int? organizationId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        OrganizationId = organizationId;
    }
}
