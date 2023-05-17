using System;
using ArmedMFG.PublicApi.Modules.ProductionReports.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class UpdateProductionReportResponse : BaseResponse
{
    public UpdateProductionReportResponse(Guid correlationid) : base(correlationid) { }
    public UpdateProductionReportResponse() { }

    public ProductionReportDto? ProductionReport { get; set; }
}
