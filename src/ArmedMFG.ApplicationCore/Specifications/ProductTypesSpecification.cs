using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductTypesSpecification : Specification<ProductType>
{
    public ProductTypesSpecification(params int[] ids)
    {
        Query.Where(p => ids.Contains(p.Id));
    }
}
