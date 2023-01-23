using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialTypeFilterSpecification : Specification<MaterialType>
{
    public MaterialTypeFilterSpecification(int? materialCategoryId)
    {
        Query.Where(p => (!materialCategoryId.HasValue || p.MaterialCategoryId == materialCategoryId));
    }
}
