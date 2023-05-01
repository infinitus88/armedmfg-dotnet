namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class GetCustomerOptionsRequest : BaseRequest
{
    public string? FullName { get; set; }

    public GetCustomerOptionsRequest(string fullName)
    {
        FullName = fullName ?? "";
    }
}
