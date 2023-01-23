namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class GetByIdProductTypeRequest : BaseRequest
{
    public int ProductTypeId { get; init; }

    public GetByIdProductTypeRequest(int productTypeId)
    {
        ProductTypeId = productTypeId;
    }
}
