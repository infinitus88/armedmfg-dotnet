using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class ListPagedProductBatchRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ListPagedProductBatchRequest(int? pageSize, int? pageIndex, DateTime? startDate, DateTime? endDate)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        StartDate = startDate;
        EndDate = endDate;
    }
}
