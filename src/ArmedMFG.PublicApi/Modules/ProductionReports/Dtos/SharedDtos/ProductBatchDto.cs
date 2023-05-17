using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductionReports.Dtos.SharedDtos;

public class ProductionReportDto
{
    public int Id { get; set; }
    public string? ReportDate { get; set; }
    public List<ProducedProductDto> ProducedProducts { get; set; } = new List<ProducedProductDto>();
}
