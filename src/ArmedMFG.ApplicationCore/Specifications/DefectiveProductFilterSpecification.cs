using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;

namespace ArmedMFG.ApplicationCore.Specifications;

public class DefectiveProductFilterSpecification : Specification<DefectiveProduct>
{
    public DefectiveProductFilterSpecification()
    {
        Query
            .Include(dp => dp.ProductType);
    }
}
