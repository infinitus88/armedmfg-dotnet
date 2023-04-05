using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class FindListPagedCustomerResponse : BaseResponse
{
    public FindListPagedCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedCustomerResponse()
    {
    }

    public List<CustomerInfoDto> Customers { get; set; } = new List<CustomerInfoDto>();
    public int TotalCount { get; set; }
}
