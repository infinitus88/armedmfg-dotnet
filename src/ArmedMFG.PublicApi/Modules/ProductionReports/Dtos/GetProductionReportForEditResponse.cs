using System;
using ArmedMFG.PublicApi.Modules.ProductionReports.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class GetProductionReportForEditResponse : BaseResponse
{
    public GetProductionReportForEditResponse(Guid correlationId) : base(correlationId) { }
    public GetProductionReportForEditResponse() { }

    public ProductionReportForEditDto? ProductionReport { get; set; }
}
