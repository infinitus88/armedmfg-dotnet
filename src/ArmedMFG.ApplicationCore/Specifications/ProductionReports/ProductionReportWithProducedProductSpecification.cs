using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProductionReports;

public class ProductionReportWithProducedProductSpecification : Specification<ProductionReport>
{
    public ProductionReportWithProducedProductSpecification(int reportId)
    {
        Query.Where(r => r.Id == reportId)
            .Include(r => r.ProducedProducts)
            .ThenInclude(p => p.Product)
            .ThenInclude(p => p.ProductMaterials);
    }
}
