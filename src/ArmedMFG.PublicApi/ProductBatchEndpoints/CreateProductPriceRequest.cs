using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class CreateProductBatchRequest : BaseRequest
{
    public DateTime ProducedDate { get; set; }
    public List<ProducedPro>
}
