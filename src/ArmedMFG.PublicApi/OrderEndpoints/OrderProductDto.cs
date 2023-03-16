namespace ArmedMFG.PublicApi.OrderEndpoints;

public class OrderProductDto
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
    public bool HaveSingleTimePrice { get; set; }
    public decimal SingleTimePrice { get; set; }
}

public class OrderProductInfoDto
{
    public int Id { get; set; }
    public string ProductTypeName { get; set; }
    public int Quantity { get; set; }
}
