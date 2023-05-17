using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos;

public class CreateProductionReportRequest : BaseRequest
{
    public string? ReportDate { get; set; }

    public IList<ProducedProductForCreation>? ProducedProducts { get; set; } 
}

public class ProducedProductForCreation
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
}
