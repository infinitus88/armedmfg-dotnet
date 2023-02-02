namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class GetByIdProductBatchRequest : BaseRequest
{
    public int ProductBatchId { get; init; }

    public GetByIdProductBatchRequest(int productBatchId)
    {
        ProductBatchId = productBatchId;
    }
}
