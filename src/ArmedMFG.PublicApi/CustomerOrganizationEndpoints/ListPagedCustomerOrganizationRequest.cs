namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class ListPagedCustomerOrganizationRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }

    public ListPagedCustomerOrganizationRequest(int? pageSize, int? pageIndex)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
    }
}
