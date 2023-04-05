using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductCheckPointFilterSpecification : Specification<ProductCheckPoint>
{
    public ProductCheckPointFilterSpecification(DateTime? startDate, DateTime? endDate, string name)
    {
        Query
            .Include(cp => cp.ProductType)
            .Where(cp => (!startDate.HasValue || cp.CheckedDate >= startDate)
                         && (!endDate.HasValue || cp.CheckedDate <= endDate)
                         && (cp.ProductType.Name.ToLower().Contains(name.ToLower())));
    }
}
