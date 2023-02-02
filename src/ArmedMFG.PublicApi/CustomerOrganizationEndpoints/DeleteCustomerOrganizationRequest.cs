namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class DeleteCustomerOrganizationRequest : BaseRequest
{
    public int OrganizationId { get; set; }

    public DeleteCustomerOrganizationRequest(int organizationId)
    {
        OrganizationId = organizationId;
    }
}
