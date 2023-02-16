namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class ListAutocompleteCustomerRequest : BaseRequest
{
    public string? FullName { get; set; }

    public ListAutocompleteCustomerRequest(string fullName)
    {
        FullName = fullName ?? "";
    }
}
