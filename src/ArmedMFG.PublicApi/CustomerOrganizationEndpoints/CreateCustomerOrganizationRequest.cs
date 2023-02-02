using ArmedMFG.ApplicationCore.Entities;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class CreateCustomerOrganizationRequest : BaseRequest
{
    public string Name { get; set; }
    public string TIN { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Address MainBranchAddress { get; set; }
    public string Description { get; set; }
}
