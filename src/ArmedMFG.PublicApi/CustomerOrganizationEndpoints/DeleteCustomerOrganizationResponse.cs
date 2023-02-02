using System;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class DeleteCustomerOrganizationResponse : BaseResponse
{
    public DeleteCustomerOrganizationResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteCustomerOrganizationResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
