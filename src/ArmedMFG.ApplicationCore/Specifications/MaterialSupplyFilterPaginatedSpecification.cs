using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialSupplyFilterPaginatedSpecification : Specification<MaterialSupply>
{
    public MaterialSupplyFilterPaginatedSpecification(int skip, int take, int? materialTypeId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(t => (!materialTypeId.HasValue || t.MaterialTypeId == materialTypeId))
            .Skip(skip).Take(take);
    }
}
