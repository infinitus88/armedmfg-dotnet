namespace ArmedMFG.PublicApi.OrderEndpoints;

public class DeleteOrderRequest : BaseRequest
{
    public int OrderId { get; set; }

    public DeleteOrderRequest(int orderId)
    {
        OrderId = orderId;
    }
}
