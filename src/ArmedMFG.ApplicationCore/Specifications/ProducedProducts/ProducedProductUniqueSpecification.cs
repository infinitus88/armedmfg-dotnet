using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProducedProducts;

public class ProducedProductUniqueSpecification : Specification<ProducedProduct>
{
    public ProducedProductUniqueSpecification(int productionReportId, int productId)
        : base()
    {
        Query
            .Where(p => p.ProductionReportId == productionReportId && p.ProductId == productId);
    }
}
