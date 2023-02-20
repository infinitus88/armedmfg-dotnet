using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProducedProductFilterSpecification : Specification<ProducedProduct>
{
    public ProducedProductFilterSpecification(DateTime? startDate, DateTime? endDate, int? productTypeId)
    {
        Query
            .Include(p => p.ProductBatch)
            .Where(p => (!startDate.HasValue || p.ProductBatch.ProducedDate.Date >= startDate.Value.Date)
                        && (!endDate.HasValue || p.ProductBatch.ProducedDate.Date <= endDate.Value.Date)
                        && (!productTypeId.HasValue || p.ProductTypeId == productTypeId));
    }

    public ProducedProductFilterSpecification()
    {
        Query
            .Include(p => p.ProductBatch);
    }
}
