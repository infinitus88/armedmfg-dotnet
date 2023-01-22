using Ardalis.Specification;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductTypeNameSpecification : Specification<ProductType>
{
    public ProductTypeNameSpecification(string productTypeName)
    {
        Query.Where(productType => productTypeName == productType.Name);
    }
}
