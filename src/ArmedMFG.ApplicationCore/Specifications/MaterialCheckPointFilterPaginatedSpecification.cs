using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialCheckPointFilterPaginatedSpecification : Specification<MaterialCheckPoint>
{
    public MaterialCheckPointFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, string name)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Include(cp => cp.MaterialType)
            .Where(cp => (!startDate.HasValue || cp.CheckedDate >= startDate)
                         && (!endDate.HasValue || cp.CheckedDate <= endDate)
                         && (cp.MaterialType.Name.ToLower().Contains(name.ToLower())))
            .Skip(skip).Take(take);

    }
}
