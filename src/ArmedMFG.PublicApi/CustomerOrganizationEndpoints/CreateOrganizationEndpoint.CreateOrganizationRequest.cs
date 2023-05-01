using ArmedMFG.ApplicationCore.Entities;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class CreateOrganizationRequest : BaseRequest
{
    public string Name { get; set; }
    public string TaxpayerIdNum { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Region { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
    public string Description { get; set; }
}
