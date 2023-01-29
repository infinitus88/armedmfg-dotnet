using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductBatchFilterPaginatedSpecification : Specification<ProductBatch>
{
    public ProductBatchFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(b => (!startDate.HasValue || b.ProducedDate >= startDate)
                    && (!endDate.HasValue || b.ProducedDate <= endDate))
            .Skip(skip).Take(take);
    }
}
