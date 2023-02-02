namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class GetByIdCustomerOrganizationRequest : BaseRequest
{
    public int OrganizationId { get; init; }

    public GetByIdCustomerOrganizationRequest(int organizationId)
    {
        OrganizationId = organizationId;
    }
}
