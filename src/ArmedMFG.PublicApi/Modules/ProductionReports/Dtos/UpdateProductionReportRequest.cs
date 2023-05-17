namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class UpdateProductionReportRequest : BaseRequest
{
    public int Id { get; set; }
    public string? ReportDate { get; set;}
}
