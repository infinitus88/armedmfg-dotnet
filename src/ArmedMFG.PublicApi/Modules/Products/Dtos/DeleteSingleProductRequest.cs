namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class DeleteSingleProductRequest : BaseRequest
{
    public int ProductId { get; set; }

    public DeleteSingleProductRequest(int productId)
    {
        ProductId = productId;
    }
}
