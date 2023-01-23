using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductTypeNameSpecification : Specification<ProductType>
{
    public ProductTypeNameSpecification(string productTypeName)
    {
        Query.Where(productType => productTypeName == productType.Name);
    }
}
