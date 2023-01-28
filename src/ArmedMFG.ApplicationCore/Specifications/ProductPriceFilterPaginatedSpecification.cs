using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductPriceFilterPaginatedSpecification : Specification<ProductPrice>
{
    public ProductPriceFilterPaginatedSpecification(int skip, int take, int? productTypeId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(t => (!productTypeId.HasValue || t.ProductTypeId == productTypeId))
            .Skip(skip).Take(take);
    }
}
