using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProducedProducts;
public class SearchProducedProductFilterSpecification : Specification<ProducedProduct>
{
    public SearchProducedProductFilterSpecification(int productionReportId, string searchText)
    {
        Query
            .Include(p => p.Product)
            .Where(p => p.ProductionReportId == productionReportId && p.Product.Name.ToLower().Contains(searchText.ToLower()));
    }
}
