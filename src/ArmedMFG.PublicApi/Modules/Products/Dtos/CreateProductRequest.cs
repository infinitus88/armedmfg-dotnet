using System.Collections;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class CreateProductRequest : BaseRequest
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public IList<CreateProductMaterialDto> ProductMaterials { get; set; } = new List<CreateProductMaterialDto>();
}

public class CreateProductMaterialDto
{
    public int MaterialId { get; set; }
    public double Amount { get; set; }
}
