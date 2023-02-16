namespace ArmedMFG.PublicApi.OrderEndpoints;

public class ListPagedOrderRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? CustomerId { get; set; }

    public ListPagedOrderRequest(int? pageSize, int? pageIndex, int? customerId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        CustomerId = customerId;
    }
}
