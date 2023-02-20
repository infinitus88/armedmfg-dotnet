using System;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class ListPagedOrderShipmentRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? OrderId { get; set; }

    public ListPagedOrderShipmentRequest(int? pageSize, int? pageIndex, DateTime? startDate, DateTime? endDate, int? orderId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        StartDate = startDate;
        EndDate = endDate;
        OrderId = orderId;
    }
}
