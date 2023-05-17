using System;
using System.Net.Cache;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProductionReports;

public class SearchProductionReportFilterPaginatedSpecification : Specification<ProductionReport>
{
    public SearchProductionReportFilterPaginatedSpecification(int skip, int take, string searchText, DateTime? startDate, DateTime? endDate)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(b => (!startDate.HasValue || b.ReportDate >= startDate)
                        && (!endDate.HasValue || b.ReportDate <= endDate))
            .Skip(skip).Take(take);
    }
}
