using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProducedProducts;

public class SearchProducedProductFilterPaginatedSpecification : Specification<ProducedProduct>
{
    public SearchProducedProductFilterPaginatedSpecification(int skip, int take, int productionReportId, string searchText)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Include(p => p.Product)
            .Where(p => p.ProductionReportId == productionReportId && p.Product.Name.ToLower().Contains(searchText.ToLower()))
            .Skip(skip).Take(take);
    }
}
