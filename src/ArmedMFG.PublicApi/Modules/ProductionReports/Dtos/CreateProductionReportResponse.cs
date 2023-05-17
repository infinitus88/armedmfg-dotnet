using System;
using ArmedMFG.PublicApi.Modules.ProductionReports.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class CreateProductionReportResponse : BaseResponse
{
    public CreateProductionReportResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProductionReportResponse()
    {
    }

    public ProductionReportDto? ProductionReport { get; set; }
}
