using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialCheckPointRecentSpecification : Specification<MaterialCheckPoint>
{
    public MaterialCheckPointRecentSpecification()
    {
        Query
            .Include(cp => cp.MaterialType)
            .OrderBy(cp => cp.CheckedDate);
    }
}
