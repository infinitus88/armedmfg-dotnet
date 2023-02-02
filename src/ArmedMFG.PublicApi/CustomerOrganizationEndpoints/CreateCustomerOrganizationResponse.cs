using System;
using ArmedMFG.PublicApi.CustomerEndpoints;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class CreateCustomerOrganizationResponse : BaseResponse
{
    public CreateCustomerOrganizationResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateCustomerOrganizationResponse()
    {
    }

    public CustomerOrganizationDto Organization { get; set; }
}
