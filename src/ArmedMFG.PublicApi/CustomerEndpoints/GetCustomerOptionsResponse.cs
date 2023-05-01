using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class GetCustomerOptionsResponse : BaseResponse
{
    public GetCustomerOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetCustomerOptionsResponse()
    {
    }

    public List<CustomerOptionDto> Customers { get; set; } = new List<CustomerOptionDto>();
}
