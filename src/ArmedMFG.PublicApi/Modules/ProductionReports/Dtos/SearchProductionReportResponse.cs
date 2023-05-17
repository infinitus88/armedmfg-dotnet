using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.ProductionReports.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class SearchProductionReportResponse : BaseResponse
{
    public SearchProductionReportResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchProductionReportResponse()
    {
    }

    public List<ProductionReportDto> ProductionReports { get; set; } = new List<ProductionReportDto>();
    public int TotalCount { get; set; }
}
