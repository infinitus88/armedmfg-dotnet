using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductCheckPointRecentSpecification : Specification<ProductCheckPoint>
{
    public ProductCheckPointRecentSpecification()
    {
        Query
            .Include(cp => cp.ProductType)
            .OrderBy(cp => cp.CheckedDate);
    }
}
