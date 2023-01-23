namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class ListPagedProductTypeRequest : BaseRequest
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? ProductCategoryId { get; set; }

    public ListPagedProductTypeRequest(int? pageSize, int? pageIndex, int? productCategoryId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        ProductCategoryId = productCategoryId;
    }
}
