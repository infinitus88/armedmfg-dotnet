namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class GetByIdProductPriceRequest : BaseRequest
{
    public int ProductPriceId { get; init; }

    public GetByIdProductPriceRequest(int productPriceId)
    {
        ProductPriceId = productPriceId;
    }
}
