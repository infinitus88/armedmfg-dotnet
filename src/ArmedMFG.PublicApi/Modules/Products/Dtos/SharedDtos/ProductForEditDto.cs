namespace ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;

public class ProductForEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int ProductCategoryId { get; set; }
    public int Quantity { get; set; }
}
