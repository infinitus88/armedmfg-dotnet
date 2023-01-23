namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class ProductTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal CurrentPrice { get; set; }
    public string PictureUri { get; set; }
    public int ProductCategoryId { get; set; }
}
