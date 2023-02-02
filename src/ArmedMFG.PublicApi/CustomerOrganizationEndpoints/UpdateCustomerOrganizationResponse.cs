using System;
using ArmedMFG.PublicApi.CustomerEndpoints;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class UpdateCustomerOrganizationResponse : BaseResponse
{
    public UpdateCustomerOrganizationResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateCustomerOrganizationResponse()
    {
    }
    
    public CustomerOrganizationDto Organization { get; set; }
}
