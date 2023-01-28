namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class ListPagedProductPriceRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? ProductTypeId { get; set; }

    public ListPagedProductPriceRequest(int? pageSize, int? pageIndex, int? productTypeId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        ProductTypeId = productTypeId;
    }
}
