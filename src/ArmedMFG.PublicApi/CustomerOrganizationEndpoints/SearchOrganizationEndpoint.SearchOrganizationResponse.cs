using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.ProductBatchEndpoints;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class SearchOrganizationResponse : BaseResponse
{
    public SearchOrganizationResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchOrganizationResponse()
    {
    }

    public List<OrganizationInfoDto> Organizations { get; set; } = new List<OrganizationInfoDto>();
    public int TotalCount { get; set; }
}
