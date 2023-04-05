using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialCheckPointFilterSpecification : Specification<MaterialCheckPoint>
{
    public MaterialCheckPointFilterSpecification(DateTime? startDate, DateTime? endDate, string name)
    {
        Query
            .Include(cp => cp.MaterialType)
            .Where(cp => (!startDate.HasValue || cp.CheckedDate >= startDate)
                         && (!endDate.HasValue || cp.CheckedDate <= endDate)
                         && (cp.MaterialType.Name.ToLower().Contains(name.ToLower())));
    }   
}
