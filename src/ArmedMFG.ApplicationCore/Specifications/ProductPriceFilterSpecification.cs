using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductPriceFilterSpecification : Specification<ProductPrice>
{
    public ProductPriceFilterSpecification(int? productTypeId)
    {
        Query.Where(p => (!productTypeId.HasValue || p.ProductTypeId == productTypeId));
    }
}
