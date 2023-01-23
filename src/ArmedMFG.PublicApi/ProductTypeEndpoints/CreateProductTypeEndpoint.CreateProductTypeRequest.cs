namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class CreateProductTypeRequest : BaseRequest
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUri { get; set; }
    public string PictureBase64 { get; set; }
    public string PictureName { get; set; }
    public decimal CurrentPrice { get; set; }
}
