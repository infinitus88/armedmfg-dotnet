namespace ArmedMFG.PublicApi.WarehouseEndpoints;

public class ListWarehouseProductItemInfoRequest : BaseRequest
{
    public int? ProductCategoryId { get; set; }

    public ListWarehouseProductItemInfoRequest(int? productCategoryId)
    {
        ProductCategoryId = productCategoryId;
    }
}
