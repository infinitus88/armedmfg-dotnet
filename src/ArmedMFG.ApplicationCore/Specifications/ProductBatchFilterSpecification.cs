using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductBatchFilterSpecification : Specification<ProductBatch>
{
    public ProductBatchFilterSpecification(DateTime? startDate, DateTime? endDate)
    {
        Query.Where(p => (!startDate.HasValue || p.ProducedDate >= startDate)
                         && (!endDate.HasValue || p.ProducedDate <= endDate));
    }
}
