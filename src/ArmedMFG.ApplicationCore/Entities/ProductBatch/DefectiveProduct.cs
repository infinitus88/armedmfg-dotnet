using System;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.ApplicationCore.Entities.ProductBatch;

public class DefectiveProduct : BaseEntity, IAggregateRoot
{
    public DateTime FoundDate { get; set; }
    public int ProductTypeId { get; private set; }
    public ProductType? ProductType { get; private set; }
    public int Quantity { get; private set; }

    public DefectiveProduct(DateTime foundDate, int productTypeId,
        int quantity)
    {
        FoundDate = foundDate;
        ProductTypeId = productTypeId;
        Quantity = quantity;
    }

}
