using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialTypeFilterPaginatedSpecification : Specification<MaterialType>
{
    public MaterialTypeFilterPaginatedSpecification(int skip, int take, int? materialCategoryId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(t => (!materialCategoryId.HasValue || t.MaterialCategoryId == materialCategoryId))
            .Skip(skip).Take(take);
    }
}
