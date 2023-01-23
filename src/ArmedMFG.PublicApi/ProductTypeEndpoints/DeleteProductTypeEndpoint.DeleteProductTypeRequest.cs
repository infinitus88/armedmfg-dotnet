namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class DeleteProductTypeRequest : BaseRequest
{
    public int ProductTypeId { get; set; }

    public DeleteProductTypeRequest(int productTypeId)
    {
        ProductTypeId = productTypeId;
    }
}
