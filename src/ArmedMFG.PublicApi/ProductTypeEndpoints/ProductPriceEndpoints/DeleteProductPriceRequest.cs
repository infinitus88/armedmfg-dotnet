namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class DeleteProductPriceRequest : BaseRequest
{
    public int ProductPriceId { get; set; }

    public DeleteProductPriceRequest(int productPriceId)
    {
        ProductPriceId = productPriceId;
    }
}
