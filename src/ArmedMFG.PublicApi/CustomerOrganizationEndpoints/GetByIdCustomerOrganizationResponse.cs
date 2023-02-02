using System;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class GetByIdCustomerOrganizationResponse : BaseResponse
{
    public GetByIdCustomerOrganizationResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdCustomerOrganizationResponse()
    {
    }

    public CustomerOrganizationDto Organization { get; set; }
}
