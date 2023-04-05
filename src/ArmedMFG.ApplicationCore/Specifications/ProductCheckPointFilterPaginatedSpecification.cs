using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductCheckPointFilterPaginatedSpecification : Specification<ProductCheckPoint>
{
    public ProductCheckPointFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, string name)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Include(cp => cp.ProductType)
            .Where(cp => (!startDate.HasValue || cp.CheckedDate >= startDate)
                         && (!endDate.HasValue || cp.CheckedDate <= endDate)
                         && (cp.ProductType.Name.ToLower().Contains(name.ToLower())))
            .Skip(skip).Take(take);

    }
}
