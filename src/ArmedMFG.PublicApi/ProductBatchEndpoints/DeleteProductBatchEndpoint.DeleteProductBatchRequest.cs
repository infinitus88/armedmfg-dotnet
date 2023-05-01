namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class DeleteProductBatchRequest : BaseRequest
{
    public int ProductBatchId { get; set; }

    public DeleteProductBatchRequest(int productBatchId)
    {
        ProductBatchId = productBatchId;
    }
}
