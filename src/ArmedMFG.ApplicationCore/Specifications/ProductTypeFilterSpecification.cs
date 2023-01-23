using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductTypeFilterSpecification : Specification<ProductType>
{
    public ProductTypeFilterSpecification(int? productCategoryId)
    {
        Query.Where(p => (!productCategoryId.HasValue || p.ProductCategoryId == productCategoryId));
    }
}
