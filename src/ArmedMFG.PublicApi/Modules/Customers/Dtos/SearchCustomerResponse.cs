using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

public class SearchCustomerResponse : BaseResponse
{
    public SearchCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchCustomerResponse()
    {
    }

    public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
    public int TotalCount { get; set; }
}
