using System;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class DeleteSingleProductionReportResponse : BaseResponse
{
    public DeleteSingleProductionReportResponse(Guid correlationId) : base(correlationId) { }
    public DeleteSingleProductionReportResponse() { }

    public string Status { get; set; } = "Deleted";
}
