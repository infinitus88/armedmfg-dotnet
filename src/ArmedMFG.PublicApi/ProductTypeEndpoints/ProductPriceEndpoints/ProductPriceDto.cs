using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class ProductPriceDto
{
    public int Id { get; set; }
    public DateTime FromDate { get; set; }
    public decimal Price { get; set; }
    public int ProductTypeId { get; set; }
}
