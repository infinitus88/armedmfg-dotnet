using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class ListPagedCustomerOrganizationResponse : BaseResponse
{
    public ListPagedCustomerOrganizationResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedCustomerOrganizationResponse()
    {
    }

    public List<CustomerOrganizationDto> Organizations { get; set; } = new List<CustomerOrganizationDto>();
    public int PageCount {get; set; }
}
