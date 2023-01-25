using ArmedMFG.BlazorShared.Attributes;

namespace ArmedMFG.BlazorShared.Models;

[Endpoint(Name = "product-categories")]
public class ProductCategory : LookupData
{
    public int DepartmentId { get; set; }
}
