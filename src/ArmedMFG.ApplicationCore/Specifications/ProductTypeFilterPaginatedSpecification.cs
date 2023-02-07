using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductTypeFilterPaginatedSpecification : Specification<ProductType>
{
    public ProductTypeFilterPaginatedSpecification(int skip, int take, int? productCategoryId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(t => (!productCategoryId.HasValue || t.ProductCategoryId == productCategoryId))
            .Skip(skip).Take(take)
            .Include(t => t.ProductPrices);
    }
}
