using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints.ProducedProductEndpoints;

public class ListPagedProducedProductRequest : BaseRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? ProductTypeId { get; set; }

    public ListPagedProducedProductRequest(DateTime? startDate, DateTime? endDate, int? productTypeId)
    {
        StartDate = startDate;
        EndDate = endDate;
        ProductTypeId = productTypeId;
    }
}
