namespace ArmedMFG.PublicApi.OrderEndpoints;

public class ShipmentProductDto
{
    public int Id { get; set; }
    public int OrderShipmentId { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
}
