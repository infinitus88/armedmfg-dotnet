using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProductionReports;

public class SearchProductionReportFilterSpecification : Specification<ProductionReport>
{
    public SearchProductionReportFilterSpecification(string searchText, DateTime? startDate, DateTime? endDate)
    {
        Query.Where(p => (!startDate.HasValue || p.ReportDate >= startDate)
                         && (!endDate.HasValue || p.ReportDate <= endDate));
    }
}
