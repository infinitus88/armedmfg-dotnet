using System;
using ArmedMFG.PublicApi.CustomerEndpoints;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class CreateOrganizationResponse : BaseResponse
{
    public CreateOrganizationResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateOrganizationResponse()
    {
    }

    public OrganizationInfoDto Organization { get; set; }
}
