namespace ArmedMFG.PublicApi.OrderEndpoints;

public class GetOrderDetailRequest : BaseRequest
{
    public int OrderId { get; set; }

    public GetOrderDetailRequest(int orderId)
    {
        OrderId = orderId;
    }
}
