namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class GetProductForEditRequest : BaseRequest
{
    public int ProductId { get; init; }

    public GetProductForEditRequest(int productId)
    {
        ProductId = productId;
    }
}
