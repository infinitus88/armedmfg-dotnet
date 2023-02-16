using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class ListPagedCustomerResponse : BaseResponse
{
    public ListPagedCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedCustomerResponse()
    {
    }

    public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
    public int PageCount {get; set; }
}
