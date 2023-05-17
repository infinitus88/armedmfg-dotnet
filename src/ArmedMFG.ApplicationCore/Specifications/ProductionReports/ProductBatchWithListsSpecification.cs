using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProductionReports;

public sealed class ProductBatchWithListsSpecification : Specification<ProductionReport>, ISingleResultSpecification
{
    public ProductBatchWithListsSpecification(int productBatchId)
    {
        Query
            .Where(b => b.Id == productBatchId)
            .Include(b => b.ProducedProducts);
    }
}
