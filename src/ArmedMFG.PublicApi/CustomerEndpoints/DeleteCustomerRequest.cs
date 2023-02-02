namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class DeleteCustomerRequest : BaseRequest
{
    public int CustomerId { get; set; }

    public DeleteCustomerRequest(int customerId)
    {
        CustomerId = customerId;
    }
}
