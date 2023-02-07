namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class GetByIdCustomerRequest : BaseRequest
{
    public int CustomerId { get; init; }

    public GetByIdCustomerRequest(int customerId)
    {
        CustomerId = customerId;
    }
}
