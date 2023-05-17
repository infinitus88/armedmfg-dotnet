using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductionReport;

public class ProductionReport : BaseEntity, IAggregateRoot
{
    public DateTime ReportDate { get; set; }

    private readonly List<ProducedProduct> _producedProducts = new List<ProducedProduct>();
    
    public IReadOnlyCollection<ProducedProduct> ProducedProducts => _producedProducts.AsReadOnly();
    
    public ProductionReport(DateTime reportDate)
    {
        ReportDate = reportDate;
    }
       
    public void AddProductRange(IList<ProducedProduct> producedProducts)
    {
        foreach (var product in producedProducts) 
        {
            _producedProducts.Add(product);
        }
    } 
    
    public void UpdateDetails(ProductionReportDetails details)
    {
        Guard.Against.Default(details.ReportDate, nameof(details.ReportDate));

        ReportDate = details.ReportDate;

        
    }
    
    public readonly record struct ProductionReportDetails
    {
        public DateTime ReportDate { get; init; }

        public ProductionReportDetails(DateTime reportDate)
        {
            ReportDate = reportDate;
        }
    }
}
